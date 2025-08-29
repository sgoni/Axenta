namespace Accounting.Application.Common.Interfaces;

public interface IJournalEntryRepository
{
    Task<bool> PeriodAlreadyReversedAsync(Guid periodId);
    Task<bool> PeriodAlreadyClosedAsync(Guid periodId);
}