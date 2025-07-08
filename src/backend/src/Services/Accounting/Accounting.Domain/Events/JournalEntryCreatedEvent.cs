namespace Accounting.Domain.Events;

public record JournalEntryCreatedEvent(JournalEntry journalEntry) : IDomainEvent;