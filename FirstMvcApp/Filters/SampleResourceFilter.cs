using Microsoft.AspNetCore.Mvc.Filters;

namespace FirstMvcApp.Filters;

public class SampleResourceFilter : Attribute, IResourceFilter
{
    public void OnResourceExecuting(ResourceExecutingContext context)
    {
        context.HttpContext.Response.Cookies.Append("LastVisit", DateTime.Now.ToString("R"));
    }

    public void OnResourceExecuted(ResourceExecutedContext context)
    {
        //throw new NotImplementedException();
    }
}