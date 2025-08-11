namespace Accounting.Application.Accounting.Companies.Queries.GetCompanies;

public class GetCompaniesHandler(IApplicationDbContext dbContext)
    : IQueryHandler<GetCompaniesQuery, GetCompaniesQueryResult>
{
    public async Task<GetCompaniesQueryResult> Handle(GetCompaniesQuery query, CancellationToken cancellationToken)
    {
        // get xompanies with pagination
        // return result

        var pageIndex = query.PaginationRequest.PageIndex;
        var pageSize = query.PaginationRequest.PageSize;
        var totalCount = await dbContext.Accounts.LongCountAsync(cancellationToken);

        var companies = await dbContext.Companies
            .AsNoTracking()
            .Skip(pageIndex * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        return new GetCompaniesQueryResult(
            new PaginatedResult<CompanyDto>(pageIndex,
                pageSize,
                totalCount,
                companies.ToCompanyDtoList()));
    }
}