namespace Accounting.Application.Accounting.Periods.EventHandlers.Domain;

public class PeriodReopenedEventHandler(
    IApplicationDbContext dbContext,
    ICurrentUserService currentUserService,
    IPublishEndpoint publishEndpoint,
    ILogger<PeriodReopenedEventHandler> logger) : INotificationHandler<PeriodReopenedDomainEvent>
{
    public async Task Handle(PeriodReopenedDomainEvent domainEvent, CancellationToken cancellationToken)
    {
        logger.LogInformation("Domain Event handled: {DomainEvent}", domainEvent.GetType().Name);
        var periodDomainEvent = domainEvent.PeriodId;

        var auditLog = CreateNewAuditLog(periodDomainEvent);
        dbContext.AuditLogs.Add(auditLog);
        await dbContext.SaveChangesAsync(cancellationToken);

        // Notification event
        var emailEvent = new EmailNotificationIntegrationEvent(
            Guid.NewGuid(),
            DateTime.UtcNow,
            "auditor@empresa.com",
            "Accounting period reopening",
            $"The period {domainEvent.Year}-{domainEvent.Month} has been reopened by {domainEvent.ReopenedBy}."
        );

        await publishEndpoint.Publish(emailEvent, cancellationToken);
    }

    private AuditLog CreateNewAuditLog(Guid periodId)
    {
        var auditLog = AuditLog.Create(
            AuditLogId.Of(Guid.NewGuid()),
            "Period",
            EntityId.Of(PeriodId.Of(periodId).Value),
            JournalEntryType.Reversal.Name,
            PerformedBy.Of(new Guid("d1521f2b-7690-467d-9fe3-4d2ee00f6950")),
            $"Period reopened {periodId}"
        );

        return auditLog;
    }
}