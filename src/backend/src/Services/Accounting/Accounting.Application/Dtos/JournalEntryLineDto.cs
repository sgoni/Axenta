namespace Accounting.Application.Dtos;

public record JournalEntryLineDto(
    Guid Id,
    Guid JournalEntryId,
    Guid AccountId,
    decimal Debit,
    decimal Credit,
    int LineNumber);