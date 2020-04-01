using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Cypal.TagHelpers
{

    [HtmlTargetElement("span", Attributes = AttributeName)]
    [HtmlTargetElement("div", Attributes = AttributeName)]
    [HtmlTargetElement("li", Attributes = AttributeName)]
    [HtmlTargetElement("ul", Attributes = AttributeName)]
    public class HiddenTagHelper : AbstractRolebasedTagHelper
    {
        private const string AttributeName = "asp-hidden-for";

        public HiddenTagHelper(IHttpContextAccessor contextAccessor):base(contextAccessor)
        {
        }

        public override string GetAttributeName()
        {
            return AttributeName;
        }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {

            await base.ProcessAsync(context, output);

            if (ShouldHide(context))
                output.Hide();
        }

        private bool ShouldHide(TagHelperContext context) => UserHasRole(context);

    }
}
