namespace Accounting.Application.Accounting.Periods.EventHandlers.IntegrationEvent;

public class PeriodClosedIntegrationEventConsumer(
    IApplicationDbContext dbContext,
    ISender sender,
    ILogger<PeriodClosedIntegrationEventConsumer> logger) : IConsumer<PeriodClosedIntegrationEvent>
{
    public async Task Consume(ConsumeContext<PeriodClosedIntegrationEvent> context)
    {
        var @event = context.Message;

        // --- Idempotence --- 
        // Avoid processing the same event twice (if the rententred broker). 
        // Options: 
        // 1) keep a "eventlog" with Messageid (context.messageid) processed. 
        // 2) Verify if there are already the reversals of the period.

        // TO DO
        // Create accounts for defect of the countable cierre

        throw new NotImplementedException();
    }
}