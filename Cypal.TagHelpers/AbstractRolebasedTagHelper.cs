using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Cypal.TagHelpers
{
    public abstract class AbstractRolebasedTagHelper : TagHelper
    {
        public abstract string GetAttributeName();

        private readonly IHttpContextAccessor contextAccessor;

        public AbstractRolebasedTagHelper(IHttpContextAccessor contextAccessor)
        {
            this.contextAccessor = contextAccessor;
        }

        protected bool UserHasRole(TagHelperContext context)
        {
            string[] roles = GetRoles(context);

            return LoggedInUser.IsInAnyOfRoles(roles);
        }

        private string[] GetRoles(TagHelperContext context)
        {
            var rolesAttribute = context.AllAttributes.Single(x => x.Name == GetAttributeName());
            var rolesString = (rolesAttribute.Value as HtmlString).Value;

            var roles = rolesString.SplitAsCsv();
            return roles;
        }

        private ClaimsPrincipal LoggedInUser => contextAccessor.HttpContext.User;
    }
}
