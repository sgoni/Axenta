namespace Accounting.Domain.Events;

public record PeriodReopenedDomainEvent(Guid PeriodId, int Year, int Month, string? ClosedBy = null) : IDomainEvent;