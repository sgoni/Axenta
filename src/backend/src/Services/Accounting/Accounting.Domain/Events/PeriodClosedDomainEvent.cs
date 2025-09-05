namespace Accounting.Domain.Events;

public record PeriodClosedDomainEvent(Guid PeriodId, int Year, int Month, string? ClosedBy = null) : IDomainEvent;