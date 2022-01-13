using Microsoft.AspNetCore.Razor.TagHelpers;

namespace FirstMvcApp.TagHelpers
{
    public class CustomAddressTagHelper : TagHelper
    {
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "h2";
            output.TagMode = TagMode.SelfClosing;
            output.PreContent.SetContent("<div>");
            output.Content.SetContent("123");
            output.PostContent.SetContent("</div>");

            //var data = await Task.Run(() => 15);

            //output.Content.Append(Environment.NewLine + data);
        }
    }
}
