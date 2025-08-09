namespace Accounting.Domain.Events;

public record PeriodClosedDomainEvent(Guid PeriodId) : IDomainEvent;