using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.DependencyInjection;

namespace Cypal.MultiTenants
{
    public static class TenantConfigurator
    {
        public static void AddMultiTenantSupport(this IServiceCollection services)
        {
            services.AddSingleton<TenantFinder, TenantFinder>();

            services.AddScoped<Tenant, Tenant>(serviecProvider => {
                var contextAccessor = serviecProvider.GetRequiredService<IHttpContextAccessor>();
                var identifier = serviecProvider.GetRequiredService<TenantFinder>();
                var tenant = identifier.Find(contextAccessor.HttpContext);
                return tenant;
            });

            services.Configure<RazorViewEngineOptions>(options =>
            {
                options.ViewLocationExpanders.Insert(0, new TenantViewLocationExpander());
            });

        }
    }

}
