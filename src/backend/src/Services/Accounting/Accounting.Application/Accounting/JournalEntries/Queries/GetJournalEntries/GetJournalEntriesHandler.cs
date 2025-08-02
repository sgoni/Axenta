namespace Accounting.Application.Accounting.JournalEntries.Queries.GetJournalEntries;

public class GetJournalEntriesHandler(IApplicationDbContext dbContext)
    : IQueryHandler<GetJournalEntriesQuery, GetJournalEntriesResult>
{
    public async Task<GetJournalEntriesResult> Handle(GetJournalEntriesQuery query,
        CancellationToken cancellationToken)
    {
        var pageIndex = query.PaginationRequest.PageIndex;
        var pageSize = query.PaginationRequest.PageSize;
        var totalCount = await dbContext.Accounts.LongCountAsync(cancellationToken);

        var journalEntries = await dbContext.JournalEntries
            .Include(je => je.JournalEntryLines)
            .AsNoTracking()
            .OrderBy(je => je.Date)
            .Skip(pageIndex * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        return new GetJournalEntriesResult(
            new PaginatedResult<JournalEntryDto>(
                pageIndex,
                pageSize,
                totalCount,
                journalEntries.ToJournalEntryDtoList()));
    }
}