using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Cypal.TagHelpers
{
    [HtmlTargetElement("input", Attributes = AttributeName)]
    [HtmlTargetElement("textarea", Attributes = AttributeName)]
    [HtmlTargetElement("select", Attributes = AttributeName)]
    public class EditableTagHelper : AbstractRolebasedTagHelper
    {
        private const string AttributeName = "asp-editable-for";

        public EditableTagHelper(IHttpContextAccessor contextAccessor):base(contextAccessor)
        {
        }
        public override string GetAttributeName()
        {
            return AttributeName;
        }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            await base.ProcessAsync(context, output);

            if (ShouldBeEditable(context))
                return;

            output.MakeReadOnly();
            output.AddHelpText();
        }


        private bool ShouldBeEditable(TagHelperContext context) => UserHasRole(context);
    }
}
