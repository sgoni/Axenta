namespace Accounting.Domain.Events;

public record JournalEntryUpdatedEvent(JournalEntry Before, JournalEntry After) : IDomainEvent;