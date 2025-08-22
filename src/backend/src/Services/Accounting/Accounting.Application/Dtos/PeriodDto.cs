namespace Accounting.Application.Dtos;

public record PeriodDto(
    Guid Id,
    Guid CompanyId,
    int Year,
    int Month,
    DateTime StartDate,
    DateTime EndDate,
    bool IsClosed);