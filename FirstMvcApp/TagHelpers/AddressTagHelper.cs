using Microsoft.AspNetCore.Razor.TagHelpers;

namespace FirstMvcApp.TagHelpers
{
    public class Address1TagHelper : TagHelper
    {
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "h2";
            output.TagMode = TagMode.SelfClosing;

            output.Content.SetContent($"76 Buckingham Palace Road, London SW1W - {DateTime.Now:F}");

            //var data = await Task.Run(() => 15);

            //output.Content.Append(Environment.NewLine + data);
        }
    }
}
