using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace FirstMvcApp.TagHelpers
{
    public class GetNewsButtonTagHelper : TagHelper
    {
        private readonly IActionContextAccessor _contextAccessor;

        public GetNewsButtonTagHelper(IActionContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            var user = _contextAccessor.ActionContext?.HttpContext.User;

            if (user != null && user.IsInRole("Admin"))
            {
                output.TagName = "div";
                output.Attributes.Add("class", "row");
                output.Content.SetContent("Admin");
            }
        }


    }
}
