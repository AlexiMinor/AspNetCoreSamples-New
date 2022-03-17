using Microsoft.AspNetCore.Razor.TagHelpers;

namespace FirstMvcApp.TagHelpers
{
    public class PaginatorTagHelper : TagHelper
    {
        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "div";
            output.Attributes.Add("class", "btn-group");
            var target = await output.GetChildContentAsync();
            output.Content.SetHtmlContent(target.GetContent());
        }
    }
}
