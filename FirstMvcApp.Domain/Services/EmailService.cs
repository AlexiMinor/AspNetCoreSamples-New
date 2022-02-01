using FirstMvcApp.Core.Interfaces;
using Microsoft.Extensions.Configuration;

namespace FirstMvcApp.Domain.Services;

public class EmailService : IEmailSender
{
    //private readonly string _fromEmail;
    //private readonly string _emailPassword;

    private readonly IConfiguration _configuration;

    public EmailService(IConfiguration configuration)
    {
        _configuration = configuration;
        //_fromEmail = _configuration["EmailFrom"];
        //_emailPassword = _configuration["EmailPassword"];
    }

    public async Task<bool> SendEmailAsync(string subject, string emailBody, string to)
    {
        var z = _configuration.GetSection("EmailSettings")["EmailFrom"];
        return true;
    }
}