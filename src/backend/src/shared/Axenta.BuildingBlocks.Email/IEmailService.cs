namespace Axenta.BuildingBlocks.Email;

public interface IEmailService
{
    Task SendEmailAsync(string to, string subject, string body);
    Task SendEmailAsync(string to, string subject, string templatePath, string body);
}