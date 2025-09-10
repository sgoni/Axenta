using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;

namespace Axenta.BuildingBlocks.Email;

public class EmailSender : IEmailSender
{
    private readonly MailSettings _settings;

    public EmailSender(IOptions<MailSettings> options)
    {
        _settings = options.Value;
    }

    public async Task SendEmailAsync(string to, string subject, string body, bool isHtml = true, string? cc = null,
        string? bcc = null, CancellationToken ct = default)
    {
        var email = new MimeMessage();
        email.Sender = MailboxAddress.Parse(_settings.From);
        email.To.Add(MailboxAddress.Parse(to));
        if (!string.IsNullOrWhiteSpace(cc)) email.Cc.Add(MailboxAddress.Parse(cc));
        if (!string.IsNullOrWhiteSpace(bcc)) email.Bcc.Add(MailboxAddress.Parse(bcc));
        email.Subject = subject;
        email.Body = new TextPart(isHtml ? "html" : "plain") { Text = body };

        using var smtp = new SmtpClient();
        await smtp.ConnectAsync(_settings.Host, _settings.Port, SecureSocketOptions.StartTls, ct);
        await smtp.AuthenticateAsync(_settings.UserName, _settings.Password, ct);
        await smtp.SendAsync(email, ct);
        await smtp.DisconnectAsync(true, ct);
    }

    public async Task SendEmailAsync(string to, string subject, string templatePath, string body, bool isHtml = true,
        string? cc = null, string? bcc = null, CancellationToken ct = default)
    {
        var content = LoadTemplate(templatePath, body);
        var email = new MimeMessage();
        email.Sender = MailboxAddress.Parse(_settings.From);
        email.To.Add(MailboxAddress.Parse(to));
        if (!string.IsNullOrWhiteSpace(cc)) email.Cc.Add(MailboxAddress.Parse(cc));
        if (!string.IsNullOrWhiteSpace(bcc)) email.Bcc.Add(MailboxAddress.Parse(bcc));
        email.Subject = subject;
        email.Body = new TextPart(isHtml ? "html" : "plain") { Text = body };

        var builder = new BodyBuilder
        {
            HtmlBody = content
        };

        using var smtp = new SmtpClient();
        await smtp.ConnectAsync(_settings.Host, _settings.Port, SecureSocketOptions.StartTls, ct);
        await smtp.AuthenticateAsync(_settings.UserName, _settings.Password, ct);
        await smtp.SendAsync(email, ct);
        await smtp.DisconnectAsync(true, ct);
    }

    private string LoadTemplate(string templatePath, string content)
    {
        if (!File.Exists(templatePath))
            throw new FileNotFoundException("No se encontró la plantilla de correo.", templatePath);

        var templateContent = File.ReadAllText(templatePath);
        return templateContent.Replace("{{content}}", content);
    }
}