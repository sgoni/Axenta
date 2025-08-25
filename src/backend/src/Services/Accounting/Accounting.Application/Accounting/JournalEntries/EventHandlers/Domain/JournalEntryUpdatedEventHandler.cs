namespace Accounting.Application.Accounting.JournalEntries.EventHandlers.Domain;

public class JournalEntryUpdatedEventHandler(
    IApplicationDbContext dbContext,
    ICurrentUserService currentUserService,
    ILogger<JournalEntryUpdatedEventHandler> logger)
    : INotificationHandler<JournalEntryUpdatedEvent>
{
    public Task Handle(JournalEntryUpdatedEvent domainEvent, CancellationToken cancellationToken)
    {
        logger.LogInformation("Domain Event handled: {DomainEvent}", domainEvent.GetType().Name);

        var before = domainEvent.Before.ToJournalEntryDto();
        var after = domainEvent.After.ToJournalEntryDto();

        var details = JsonSerializer.Serialize(new
        {
            Before = new
            {
                before.Id,
                before.Date,
                before.Description,
                before.Lines
            },
            After = new
            {
                after.Id,
                after.Date,
                after.Description,
                after.Lines
            }
        });

        var auditLog = AuditLog.Create(
            AuditLogId.Of(Guid.NewGuid()),
            "JournalEntry",
            EntityId.Of(JournalEntryId.Of(after.Id).Value),
            "Update",
            PerformedBy.Of(currentUserService.UserId.Value),
            details
        );

        return Task.CompletedTask;
    }
}