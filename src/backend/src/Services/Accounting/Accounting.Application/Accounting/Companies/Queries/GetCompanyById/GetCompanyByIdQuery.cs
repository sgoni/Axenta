namespace Accounting.Application.Accounting.Companies.Queries.GetCompanyById;

public record GetCompanyByIdQuery(Guid companyId) : IQuery<GetCompanyByIdQueryResult>;

public record GetCompanyByIdQueryResult(CompanyDto CompanyDetail);