namespace Accounting.Application.Accounting.JournalEntries.Queries.GetJournalEntries;

public record GetJournalEntriesQuery(PaginationRequest PaginationRequest, Guid PeriodId, Guid CompanyId)
    : IQuery<GetJournalEntriesResult>;

public record GetJournalEntriesResult(PaginatedResult<JournalEntryDto> JournalEntries);