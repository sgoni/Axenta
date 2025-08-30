namespace Accounting.Application.Common.Interfaces;

public interface IEventLogRepository
{
    Task<bool> AlreadyProcessedAsync(Guid messageId);
    Task SaveProcessedAsync(Guid messageId);
}