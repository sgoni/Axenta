namespace Accounting.Application.Accounting.JournalEntries.Queries.GetJournalEntryById;

public class GetJournalEntryByIdHandler(IApplicationDbContext dbContext)
    : IQueryHandler<GetJournalEntryByIdQuery, GetJournalEntryByIdResult>
{
    public async Task<GetJournalEntryByIdResult> Handle(GetJournalEntryByIdQuery query,
        CancellationToken cancellationToken)
    {
        var journalEntry = await dbContext.JournalEntries
            .Include(je => je.JournalEntryLines)
            .AsNoTracking()
            .Where(je => je.Id == JournalEntryId.Of(query.JournalEntryId))
            .SingleOrDefaultAsync(cancellationToken);

        return new GetJournalEntryByIdResult(journalEntry.ToJournalEntryDto());
    }
}