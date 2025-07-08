namespace Accounting.Domain.Events;

public record JournalEntryUpdatedEcent(JournalEntry journalEntry) : IDomainEvent;