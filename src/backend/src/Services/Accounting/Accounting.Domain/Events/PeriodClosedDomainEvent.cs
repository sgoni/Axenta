namespace Accounting.Domain.Events;

public record PeriodClosedDomainEvent(Guid PeriodId, int Year, int Month, Guid CompanyId) : IDomainEvent;