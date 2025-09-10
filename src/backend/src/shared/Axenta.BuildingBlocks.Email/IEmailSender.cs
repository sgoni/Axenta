namespace Axenta.BuildingBlocks.Email;

public interface IEmailSender
{
    Task SendEmailAsync(string to, string subject, string body, bool isHtml = true, string? cc = null,
        string? bcc = null, CancellationToken ct = default);

    Task SendEmailAsync(string to, string subject, string templatePath, string body, bool isHtml = true,
        string? cc = null, string? bcc = null, CancellationToken ct = default);
}