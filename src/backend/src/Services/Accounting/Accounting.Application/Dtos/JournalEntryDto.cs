namespace Accounting.Application.Dtos;

public record JournalEntryDto(
    Guid Id,
    DateTime Date,
    string Description,
    Guid PeriodId,
    Guid CompanyId,
    string? CurrencyCode,
    decimal? ExchangeRate,
    DateOnly? ExchangeRateDate,
    bool IsPosted,
    bool IsReversed,
    List<JournalEntryLineDto> Lines);