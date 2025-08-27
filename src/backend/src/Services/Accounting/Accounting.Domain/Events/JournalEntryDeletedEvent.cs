namespace Accounting.Domain.Events;

public record JournalEntryDeletedEvent(JournalEntry journalEntry) : IDomainEvent;