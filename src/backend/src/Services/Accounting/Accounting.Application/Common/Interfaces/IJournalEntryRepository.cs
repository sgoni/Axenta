namespace Accounting.Application.Common.Interfaces;

public interface IJournalEntryRepository
{
    Task<bool> PeriodAlreadyReversedAsync(Guid periodId);
}