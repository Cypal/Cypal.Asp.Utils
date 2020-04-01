using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Cypal.TagHelpers
{
    [HtmlTargetElement("input", Attributes = AttributeName)]
    [HtmlTargetElement("textarea", Attributes = AttributeName)]
    [HtmlTargetElement("select", Attributes = AttributeName)]
    public class ReadOnlyTagHelper : AbstractRolebasedTagHelper
    {
        private const string AttributeName = "asp-readonly-for";

        public ReadOnlyTagHelper(IHttpContextAccessor contextAccessor):base(contextAccessor)
        {
        }
        public override string GetAttributeName()
        {
            return AttributeName;
        }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            await base.ProcessAsync(context, output);

            if (ShouldBeReadOnly(context))
            {
                output.MakeReadOnly();
                output.AddHelpText();
            }
        }


        private bool ShouldBeReadOnly(TagHelperContext context) => UserHasRole(context);
    }
}
