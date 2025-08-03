namespace Accounting.Application.Dtos;

public record JournalEntryDto(
    Guid Id,
    DateTime Date,
    string Description,
    bool IsCanceled,
    Guid PeriodId,
    List<JournalEntryLineDto> Lines);