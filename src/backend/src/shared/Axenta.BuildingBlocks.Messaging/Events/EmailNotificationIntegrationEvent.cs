namespace Axenta.BuildingBlocks.Messaging.Events;

public record EmailNotificationIntegrationEvent : IntegrationEvent
{
    public EmailNotificationIntegrationEvent(Guid eventId, DateTime occurredOn, string to, string subject, string body,
        string? cc = null, string? bcc = null, bool isHtml = true, string? correlationId = null)
    {
        EventId = eventId;
        OccurredOn = occurredOn;
        To = to;
        Subject = subject;
        Body = body;
        Cc = cc;
        Bcc = bcc;
        IsHtml = isHtml;
        CorrelationId = correlationId;
    }

    public Guid EventId { get; set; }
    public DateTime OccurredOn { get; set; }
    public string To { get; set; }
    public string Subject { get; set; }
    public string Body { get; set; }
    public string? Cc { get; set; }
    public string? Bcc { get; set; }
    public bool IsHtml { get; set; }
    public string? CorrelationId { get; set; }
}