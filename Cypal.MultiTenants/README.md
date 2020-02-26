# Cypal ASP Utils: Multi-Tenants

In a typical SaaS Project, you may want to "White Label" you product. Essentially brand your product based on the Customer. In that case, instead of branching out code for every brand you support, its better to maintain a single branch for all the code. It is easier for maintenance. Even during deployment, it is easier and cheaper, to deploy to a single App Server and serve all the brands via separate URLs. In the code, you can identify each "tenant" via the URL and brand the page accordingly (or customize a feature). This library helps you to provide multi-tenant support in an ASP Core application.

## Overview

A 'Tenant' is the basic customization you do on your product. The tenant class contains a name, a list of URLs associated with it, and some settings, based on which you want to customize the product (such as the product name, logo path, whether a feature is enabled or not, etc).

Installation
-------------

This library is available as a NuGet package. You can install it using the NuGet Package Console window:

```
PM> Install-Package Cypal.MultiTenants
```

Initialization of the Multi-Tenancy is simple. After installation, add this line in your Startup class: 

```csharp
public class Startup
{

    public void ConfigureServices(IServiceCollection services)
    {

        // other setup here
        ...

        services.AddMultiTenantSupport();

    }
}
```

Configuration
-----

You need to define the list of tenants, and the supported URLs in your appsettings.json (or the Azure Configuration or other supported places). The set up is typically like:

```json
{
  // other settings ...
    "Tenants": [
        {
            "name": "My Public Library",
            "hosts": ["www.my-public-library.com" ],
            "settings": 
            {
                "layout": "_PublicLibrary",
                "logo": "/images/public-library.png",
                "menu": "_PublicLibraryMenu",
                "maxBooksSupported": 10000
            }
        },
        {
            "name": "My School Library",
            "hosts": ["library.my-school.com","localhost"],
            "isDefault": true,
            "settings": 
            {
                "layout": "_MySchoolLibrary",
                "logo": "/images/my-school-library.png",
                "menu": "_MySchoolLibraryMenu",
                "maxBooksSupported": 2000
            }
        }
    ]
}
```

Usage
-----

Once you have added the tenants in your configuration, the right Tenant object will be selected based on the Request's URL and will be injected into the objects you use (Service classes, CSHTML Models, CSHTML pages, etc). If the host name in the Request URL doesn't match any of the host names mentioned in the 'hosts' property of any of the Tenant, then the Tenant with "isDefault" with value of 'true' will be returned.

```csharp
public class MyService
{

    public MyService(ApplicationDbContext context, Tenant tenant)
    {
        // the tenant object will be injected automatically

    }
}
```

Background Threads
------------------

If you want to execute any specific Background jobs, which may not have the Request or Http context, then you need to explicitly configure the 'CurrentHost' in your app setting. This is because there is no way to identify the URL of the hosted domain in the .Net Core's Startup class. (it was earlier possible with regular .Net though):

```json
{
    // other settings 
    ...
    "CurrentHost": "localhost"
}
```

When you configure this way, the first priority is still to detect the Tenant from the Request object. When it fails for any reason (when the background thread doesn't have a HTTP context), then the host name from CurrentHost configuration will be used as a fall back.

Custom Settings
-----------------
If you want to have any settigs that changes with each tenant, you can add them in the 'settings' section of your config file. All the settings are available as a Dictonary in the Tenant object. Although you can directly use the Dictonary or the Setting & IntSetting methods, I highly recommend to create extension methods for each settings:

```csharp
public static class TenantExtensions
{

        public static string Layout(this Tenant tenant)
            => tenant.Setting("Layout");

        public static string Menu(this Tenant tenant)
            => tenant.Setting("menu", "_DefaultMenu");

        public static string Logo(this Tenant tenant)
            => tenant.Setting("logo", "/images/unknown.jpg");

        public static int MaxBooks(this Tenant tenant)
            => tenant.IntSetting("maxBooksSupported", 1000).Value;

}
```