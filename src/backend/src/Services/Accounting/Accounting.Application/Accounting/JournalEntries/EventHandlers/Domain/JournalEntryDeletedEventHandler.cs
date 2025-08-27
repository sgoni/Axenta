namespace Accounting.Application.Accounting.JournalEntries.EventHandlers.Domain;

public class JournalEntryDeletedEventHandler(
    IApplicationDbContext dbContext,
    ICurrentUserService currentUserService,
    ILogger<JournalEntryDeletedEventHandler> logger)
    : INotificationHandler<JournalEntryDeletedEvent>
{
    public Task Handle(JournalEntryDeletedEvent domainEvent, CancellationToken cancellationToken)
    {
        logger.LogInformation("Domain Event handled: {DomainEvent}", domainEvent.GetType().Name);
        var journalEntryDomainEvent = domainEvent.journalEntry.ToJournalEntryDto();

        var auditLog = CreateNewAuditLog(journalEntryDomainEvent);
        dbContext.AuditLogs.Add(auditLog);
        dbContext.SaveChangesAsync(cancellationToken);
        return Task.CompletedTask;
    }

    private AuditLog CreateNewAuditLog(JournalEntryDto journalEntry)
    {
        var details = JsonSerializer.Serialize(new
        {
            journalEntry.Id,
            journalEntry.Date,
            journalEntry.Description,
            journalEntry.PeriodId,
            journalEntry.CompanyId,
            journalEntry.CurrencyCode,
            Lines = journalEntry.Lines.Select(l => new
            {
                l.AccountId,
                l.Debit,
                l.Credit,
                l.LineNumber
            })
        });

        var auditLog = AuditLog.Create(
            AuditLogId.Of(new Guid()),
            "JournalEntry",
            EntityId.Of(JournalEntryId.Of(journalEntry.Id).Value),
            "Create",
            PerformedBy.Of(currentUserService.UserId.Value),
            ""
        );

        return auditLog;
    }
}