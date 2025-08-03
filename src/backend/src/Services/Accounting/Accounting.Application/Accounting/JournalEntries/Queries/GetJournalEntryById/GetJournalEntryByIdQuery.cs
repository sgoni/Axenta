namespace Accounting.Application.Accounting.JournalEntries.Queries.GetJournalEntryById;

public record GetJournalEntryByIdQuery(Guid JournalEntryId) : IQuery<GetJournalEntryByIdResult>;

public record GetJournalEntryByIdResult(JournalEntryDto JournalEntryDetail);