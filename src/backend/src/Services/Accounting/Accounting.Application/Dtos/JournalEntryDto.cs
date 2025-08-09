namespace Accounting.Application.Dtos;

public record JournalEntryDto(
    Guid Id,
    DateTime Date,
    string Description,
    bool IsPosted,
    Guid PeriodId,
    List<JournalEntryLineDto> Lines);