using Microsoft.AspNetCore.Mvc.Filters;

namespace FirstMvcApp.Filters
{
    public class SampleActionFilterAttribute :Attribute, IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (!context.ModelState.IsValid)
            {
                context.ActionArguments["id"] = 42;
            }

            await next();
        }
    }
}
