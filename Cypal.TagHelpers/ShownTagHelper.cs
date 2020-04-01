using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Cypal.TagHelpers
{
    [HtmlTargetElement("span", Attributes = AttributeName)]
    [HtmlTargetElement("div", Attributes = AttributeName)]
    [HtmlTargetElement("li", Attributes = AttributeName)]
    [HtmlTargetElement("ul", Attributes = AttributeName)]
    public class ShownTagHelper : AbstractRolebasedTagHelper
    {
        private const string AttributeName = "asp-shown-for";

        public ShownTagHelper(IHttpContextAccessor contextAccessor):base(contextAccessor)
        {
        }

        public override string GetAttributeName()
        {
            return AttributeName;
        }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {

            await base.ProcessAsync(context, output);

            if (ShouldShow(context))
                return;

            output.Hide();
        }

        private bool ShouldShow(TagHelperContext context) => UserHasRole(context);

    }
}
