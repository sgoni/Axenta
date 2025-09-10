namespace Accounting.Application.Accounting.JournalEntries.EventHandlers.Domain;

public class JournalEntryReversedEventHandler(
    IApplicationDbContext dbContext,
    ICurrentUserService currentUserService,
    IPublishEndpoint publishEndpoint,
    ILogger<JournalEntryReversedEventHandler> logger) : INotificationHandler<JournalEntryReversedDomainEvent>
{
    public async Task Handle(JournalEntryReversedDomainEvent domainEvent, CancellationToken cancellationToken)
    {
        logger.LogInformation("Domain Event handled: {DomainEvent}", domainEvent.GetType().Name);
        var journalEntryDomainEventId = domainEvent.reverseJournalEntryId;

        var auditLog = CreateNewAuditLog(journalEntryDomainEventId);
        dbContext.AuditLogs.Add(auditLog);
        await dbContext.SaveChangesAsync(cancellationToken);

        // Notification event
        var emailEvent = new EmailNotificationIntegrationEvent(
            Guid.NewGuid(),
            DateTime.UtcNow,
            "contabilidad@empresa.com",
            "Reversal of accounting entry",
            $"The entry with ID {journalEntryDomainEventId} has been reversed on date {DateTime.UtcNow:d}."
        );
        await publishEndpoint.Publish(emailEvent, cancellationToken);
    }

    private AuditLog CreateNewAuditLog(Guid journalEntryId)
    {
        var auditLog = AuditLog.Create(
            AuditLogId.Of(Guid.NewGuid()),
            "JournalEntry",
            EntityId.Of(JournalEntryId.Of(journalEntryId).Value),
            JournalEntryType.Reversal.Name,
            PerformedBy.Of(new Guid("d1521f2b-7690-467d-9fe3-4d2ee00f6950")),
            $"Reversal of entry {journalEntryId}"
        );

        return auditLog;
    }
}