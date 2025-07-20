namespace Accounting.Application.Dtos;

public record PeriodDto(
    int Year,
    int Month,
    DateTime StartDate,
    DateTime EndDate,
    bool IsClosed);