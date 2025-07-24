namespace Accounting.Application.Dtos;

public record PeriodDto(
    Guid Id,
    int Year,
    int Month,
    DateTime StartDate,
    DateTime EndDate,
    bool IsClosed);