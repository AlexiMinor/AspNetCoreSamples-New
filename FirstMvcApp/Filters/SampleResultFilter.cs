using Microsoft.AspNetCore.Mvc.Filters;

namespace FirstMvcApp.Filters
{
    public class SampleResultFilter : Attribute, IAsyncResultFilter
    {
        public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            context.HttpContext.Response.Headers.TryAdd("ResponseTime", DateTime.Now.ToString("O"));

            await next();
        }
    }
}
