namespace Accounting.Application.Accounting.Companies.Queries.GetCompanies;

public record GetCompaniesQuery(PaginationRequest PaginationRequest) : IQuery<GetCompaniesQueryResult>;

public record GetCompaniesQueryResult(PaginatedResult<CompanyDto> Companies);