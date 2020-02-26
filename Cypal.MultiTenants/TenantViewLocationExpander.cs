using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;

namespace Cypal.MultiTenants
{
    public sealed class TenantViewLocationExpander : IViewLocationExpander
    {
        private Tenant tenant;

        public IEnumerable<string> ExpandViewLocations(ViewLocationExpanderContext context, IEnumerable<string> locations)
        {

            var newLocations = new List<string>(locations);
            newLocations.Insert(0, $"/Pages/{{1}}/{tenant.Name}/{{0}}.cshtml");
            return newLocations;
        }

        public void PopulateValues(ViewLocationExpanderContext context) 
            => tenant = context.ActionContext.HttpContext.RequestServices.GetService<Tenant>();
    }


}
