using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Linq;
using System.Security.Claims;

namespace Cypal.TagHelpers
{
    public static class Extensions
    {
        public static string[] SplitAsCsv(this string csv) 
            => csv?.Split(',', StringSplitOptions.RemoveEmptyEntries) ?? Array.Empty<string>();
        public static bool IsInAnyOfRoles(this ClaimsPrincipal user, string[] roles)
            => roles.Any(role => user.IsInRole(role));
        public static void Hide(this TagHelperOutput output) 
            => output.Content.Clear();
        public static void MakeReadOnly(this TagHelperOutput output)
            => output.Attributes.Add(new TagHelperAttribute("readonly"));
        public static void AddHelpText(this TagHelperOutput output)
        {
            var small = new TagBuilder("small");
            small.AddCssClass("form-text text-muted");
            small.InnerHtml.Append("You are not authorized to edit this field");
            output.PostElement.SetHtmlContent(small);
        }

    }
}
