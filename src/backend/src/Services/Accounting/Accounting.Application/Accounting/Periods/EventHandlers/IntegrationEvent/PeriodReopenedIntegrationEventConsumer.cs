namespace Accounting.Application.Accounting.Periods.EventHandlers.IntegrationEvent;

public class PeriodReopenedIntegrationEventConsumer(
    IApplicationDbContext dbContext,
    ISender sender,
    ILogger<PeriodReopenedIntegrationEventConsumer> logger) : IConsumer<PeriodReopenedIntegrationEvent>
{
    public async Task Consume(ConsumeContext<PeriodReopenedIntegrationEvent> context)
    {
        var @event = context.Message;

        // --- Idempotence --- 
        // Avoid processing the same event twice (if the rententred broker). 
        // Options: 
        // 1) keep a "eventlog" with Messageid (context.messageid) processed. 
        // 2) Verify if there are already the reversals of the period.

        // We obtain the closing entries of the infrastructure
        var closingEntries = await dbContext.JournalEntries
            .Include(je => je.JournalEntryLines)
            .AsNoTracking()
            .Where(je => je.PeriodId == PeriodId.Of(@event.PeriodId))
            .ToListAsync();

        foreach (var entry in closingEntries.Where(e => e.IsPosted))
        {
            var reversal = entry.Reverse();
            dbContext.JournalEntries.Add(reversal);
            dbContext.JournalEntries.Update(entry);
        }

        // Redelivery with Delay due to external dependence not available:
        // throw new DelayedRedeliveryException(TimeSpan.FromSeconds(30));
    }
}