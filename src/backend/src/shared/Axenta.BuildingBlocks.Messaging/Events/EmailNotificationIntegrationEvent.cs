namespace Axenta.BuildingBlocks.Messaging.Events;

public record EmailNotificationIntegrationEvent(
    Guid EventId,
    DateTime OccurredOn,
    string To,
    string Subject,
    string Body,
    string? Cc = null,
    string? Bcc = null,
    bool IsHtml = true,
    string? CorrelationId = null
);