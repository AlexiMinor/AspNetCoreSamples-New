using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace FirstMvcApp.Filters
{
    public class SampleExceptionFilter : Attribute, IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            var action = context.ActionDescriptor.DisplayName;
            var exStack = context.Exception.StackTrace;
            var exMessage = context.Exception.Message;

            context.Result = new ContentResult()
            {
                Content = $"In {action} was thrown exception {exMessage} {Environment.NewLine} {exStack}"
            };
            context.ExceptionHandled = true;


            //context.Result = new ViewResult(); add custom ex-500 page
        }
    }
}
