namespace Accounting.Domain.Events;

public record JournalEntryReversedDomainEvent(Guid OriginalJournalEntryId, Guid ReversalJournalEntryId) : IDomainEvent;