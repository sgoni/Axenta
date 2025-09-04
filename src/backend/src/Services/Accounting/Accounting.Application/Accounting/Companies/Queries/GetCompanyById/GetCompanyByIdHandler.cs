namespace Accounting.Application.Accounting.Companies.Queries.GetCompanyById;

public class GetCompanyByIdHandler(IApplicationDbContext dbContext)
    : IQueryHandler<GetCompanyByIdQuery, GetCompanyByIdQueryResult>
{
    public async Task<GetCompanyByIdQueryResult> Handle(GetCompanyByIdQuery query, CancellationToken cancellationToken)
    {
        var companyId = CompanyId.Of(query.companyId);
        var company = await dbContext.Companies.FindAsync([companyId], cancellationToken);

        if (company is null) throw EntityNotFoundException.For<Company>(query.companyId);

        return new GetCompanyByIdQueryResult(company.ToCompanyDto());
    }
}