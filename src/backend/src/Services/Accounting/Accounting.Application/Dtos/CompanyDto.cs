namespace Accounting.Application.Dtos;

public record CompanyDto(
    Guid Id,
    string Name,
    string TaxId,
    string Country,
    string CurrencyCode,
    bool IsActive
);