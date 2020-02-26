using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Cypal.MultiTenants
{
    public class TenantFinder 
    {
        private readonly List<Tenant> tenants;
        private readonly string currentHostInConfig;

        public TenantFinder(IConfiguration configuration)
        {
            tenants = configuration.GetSection("Tenants").Get<List<Tenant>>();
            currentHostInConfig = configuration.GetValue<string>("CurrentHost");

        }
        public Tenant Find(HttpContext context)
        {
            var host = ExtractHost(context)??currentHostInConfig;

            return FindFor(host)
                ?? FindDefault()
                ?? throw new Exception($"No tenant found for host '{host}'");
        }

        private static string ExtractHost(HttpContext context) 
            => context?.Request?.Host.Host;

        private Tenant FindDefault() 
            => tenants.SingleOrDefault(x => x.IsDefault);

        private Tenant FindFor(string host)
            => host == null 
                ? null 
                : tenants.SingleOrDefault(x => x.Hosts.Contains(host));
    }


}
