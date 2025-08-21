namespace Accounting.Domain.Events;

public record PeriodReopenedDomainEvent(Guid PeriodId) : IDomainEvent;