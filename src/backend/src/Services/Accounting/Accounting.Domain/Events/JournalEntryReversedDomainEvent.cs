namespace Accounting.Domain.Events;

public record JournalEntryReversedDomainEvent(JournalEntryId Id, JournalEntryId reversalId) : IDomainEvent;