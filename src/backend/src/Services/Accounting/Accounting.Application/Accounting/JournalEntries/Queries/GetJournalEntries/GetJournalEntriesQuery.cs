namespace Accounting.Application.Accounting.JournalEntries.Queries.GetJournalEntries;

public record GetJournalEntriesQuery(PaginationRequest PaginationRequest) : IQuery<GetJournalEntriesResult>;

public record GetJournalEntriesResult(PaginatedResult<JournalEntryDto> JournalEntries);