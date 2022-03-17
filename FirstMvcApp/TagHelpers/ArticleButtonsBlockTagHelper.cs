using System.Text;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace FirstMvcApp.TagHelpers
{
    public class ArticleButtonsBlockTagHelper : TagHelper
    {
        private readonly IActionContextAccessor _contextAccessor;

        public ArticleButtonsBlockTagHelper(IActionContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            var user = _contextAccessor.ActionContext?.HttpContext.User;

            if (user != null && user.IsInRole("Admin"))
            {
                output.TagName = "div";
                output.Attributes.Add("class", "btn-toolbar mb-3");
                output.Attributes.Add("role", "toolbar");

                //set content

                //inject controller name & action-name

            }
        }


    }
}
