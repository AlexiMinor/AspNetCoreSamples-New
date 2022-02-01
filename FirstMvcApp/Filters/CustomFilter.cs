using FirstMvcApp.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace FirstMvcApp.Filters;

public class CustomFilter : Attribute, IAsyncResourceFilter
{
    private readonly IEmailSender _emailSender;
    public CustomFilter(IEmailSender emailSender)
    {
        _emailSender = emailSender;
    }

    public async Task OnResourceExecutionAsync(ResourceExecutingContext context, ResourceExecutionDelegate next)
    {
        await _emailSender.SendEmailAsync("1","2","3");
        await next();
    }
}