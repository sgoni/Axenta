namespace Accounting.Application.Common.Repositories;

public class JournalEntryRepository : IJournalEntryRepository
{
    private readonly IApplicationDbContext _dbContext;

    public JournalEntryRepository(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<bool> PeriodAlreadyReversedAsync(Guid periodId)
    {
        return await _dbContext.JournalEntries
            .AnyAsync(e =>
                e.PeriodId == PeriodId.Of(periodId) && e.JournalEntryType.Equals(JournalEntryType.Reversal.Name));
    }

    public async Task<bool> PeriodAlreadyClosedAsync(Guid periodId)
    {
        return await _dbContext.JournalEntries
            .AnyAsync(e =>
                e.PeriodId == PeriodId.Of(periodId) && e.JournalEntryType.Equals(JournalEntryType.Closing.Name));
    }
}