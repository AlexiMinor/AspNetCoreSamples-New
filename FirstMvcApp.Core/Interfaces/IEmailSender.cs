namespace FirstMvcApp.Core.Interfaces;

public interface IEmailSender
{
    public Task<bool> SendEmailAsync(string subject, string emailBody, string to);
}