namespace Accounting.Domain.Events;

public record JournalEntryReversedDomainEvent(Guid reverseJournalEntryId) : IDomainEvent;