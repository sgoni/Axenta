namespace Accounting.Application.Extensions;

public static class CompanyExtensions
{
    public static IEnumerable<CompanyDto> ToCompanyDtoList(this IEnumerable<Company> companies)
    {
        return companies.Select(company => new CompanyDto(
            company.Id.Value,
            company.Name,
            company.TaxId,
            company.Country,
            company.CurrencyCode,
            company.IsActive
        ));
    }

    public static CompanyDto DtoFromCompany(this Company company)
    {
        return new CompanyDto(
            company.Id.Value,
            company.Name,
            company.TaxId,
            company.Country,
            company.CurrencyCode,
            company.IsActive
        );
    }
}