namespace Accounting.Application.Common.Repositories;

public class EventLogRepository : IEventLogRepository
{
    private readonly IApplicationDbContext _dbContext;

    public EventLogRepository(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<bool> AlreadyProcessedAsync(Guid messageId)
    {
        return await _dbContext.EventLogs.AnyAsync(e => e.MessageId == messageId);
    }

    public async Task SaveProcessedAsync(Guid messageId)
    {
        _dbContext.EventLogs.Add(new EventLog
        {
            Id = EventLogId.Of(Guid.NewGuid()),
            MessageId = messageId,
            ProcessedAt = DateTime.UtcNow
        });

        await _dbContext.SaveChangesAsync(cancellationToken: default);
    }
}