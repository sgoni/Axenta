namespace Accounting.Application.Dtos;

public record JournalEntryDto(Guid Id, DateTime Date, string Description, Guid PeriodId, List<JournalEntryLineDto> Lines);