namespace Accounting.Domain.Events;

public record JournalEntryUpdatedEvent(JournalEntry journalEntry) : IDomainEvent;