namespace Accounting.Domain.Events;

public record PeriodReopenedIntegrationEvent(Guid PeriodId) : IDomainEvent;