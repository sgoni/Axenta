using Action = Accounting.Domain.Enums.Action;

namespace Accounting.Application.Accounting.JournalEntries.EventHandlers.Domain;

public class JournalEntryDeletedEventHandler(
    IApplicationDbContext dbContext,
    ICurrentUserService currentUserService,
    ILogger<JournalEntryDeletedEventHandler> logger)
    : INotificationHandler<JournalEntryDeletedEvent>
{
    public async Task Handle(JournalEntryDeletedEvent domainEvent, CancellationToken cancellationToken)
    {
        logger.LogInformation("Domain Event handled: {DomainEvent}", domainEvent.GetType().Name);
        var journalEntryDomainEvent = domainEvent.journalEntry.ToJournalEntryDto();

        var auditLog = CreateNewAuditLog(journalEntryDomainEvent);
        dbContext.AuditLogs.Add(auditLog);
        await dbContext.SaveChangesAsync(cancellationToken);
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
            AuditLogId.Of(Guid.NewGuid()),
            "JournalEntry",
            EntityId.Of(JournalEntryId.Of(journalEntry.Id).Value),
            Action.DELETE.Name,
            PerformedBy.Of(new Guid("d1521f2b-7690-467d-9fe3-4d2ee00f6950")),
            details
        );

        return auditLog;
    }
}