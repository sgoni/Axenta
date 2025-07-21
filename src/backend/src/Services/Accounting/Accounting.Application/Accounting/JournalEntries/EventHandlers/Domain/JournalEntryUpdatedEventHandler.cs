namespace Accounting.Application.Accounting.JournalEntries.EventHandlers.Domain;

public class JournalEntryUpdatedEventHandler(ILogger<JournalEntryUpdatedEventHandler> logger)
    : INotificationHandler<JournalEntryCreatedEvent>
{
    public Task Handle(JournalEntryCreatedEvent notification, CancellationToken cancellationToken)
    {
        logger.LogInformation("Domain Event handled: {DomainEvent}", notification.GetType().Name);
        return Task.CompletedTask; 
    }
}