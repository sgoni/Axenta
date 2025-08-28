namespace Accounting.Application.Accounting.JournalEntries.EventHandlers.Domain;

public class JournalEntryUpdatedEventHandler(
    IApplicationDbContext dbContext,
    ICurrentUserService currentUserService,
    ILogger<JournalEntryUpdatedEventHandler> logger)
    : INotificationHandler<JournalEntryUpdatedEvent>
{
    public async Task Handle(JournalEntryUpdatedEvent domainEvent, CancellationToken cancellationToken)
    {
        logger.LogInformation("Domain Event handled: {DomainEvent}", domainEvent.GetType().Name);

        var beforeDomainEvent = domainEvent.Before.ToJournalEntryDto();
        var afterDomainEvent = domainEvent.After.ToJournalEntryDto();
        var auditLog = CreateNewAuditLog(beforeDomainEvent, afterDomainEvent);

        dbContext.AuditLogs.Add(auditLog);
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    private AuditLog CreateNewAuditLog(JournalEntryDto before, JournalEntryDto after)
    {
        var details = JsonSerializer.Serialize(new
        {
            Before = new
            {
                before.Id,
                before.Date,
                before.Description,
                before.ExchangeRate,
                before.ExchangeRateDate,
                before.CurrencyCode,
                before.PeriodId,
                before.CompanyId,
                before.JournalEntryType
                //before.Lines
            },
            After = new
            {
                after.Id,
                after.Date,
                after.Description,
                after.ExchangeRate,
                after.ExchangeRateDate,
                after.CurrencyCode,
                after.PeriodId,
                after.CompanyId,
                after.JournalEntryType
                //after.Lines
            }
        });

        var auditLog = AuditLog.Create(
            AuditLogId.Of(Guid.NewGuid()),
            "JournalEntry",
            EntityId.Of(JournalEntryId.Of(after.Id).Value),
            JournalEntryType.Adjustment.Name,
            PerformedBy.Of(new Guid("d1521f2b-7690-467d-9fe3-4d2ee00f6950")),
            details
        );

        return auditLog;
    }
}