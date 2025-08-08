namespace Accounting.Application.Dtos;

public record JournalEntryDto(
    Guid Id,
    DateTime Date,
    string Description,
    bool IsReversed,
    Guid PeriodId,
    List<JournalEntryLineDto> Lines);