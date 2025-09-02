using MapsterMapper;

namespace Accounting.Application.Accounting.CostCenters.Queries.GetCostCenterById;

public class GetCostCenterByIdHandler(IApplicationDbContext dbContext)
    : IQueryHandler<GetCostCenterByIdQuery, GetCostCenterByIdResult>
{
    public async Task<GetCostCenterByIdResult> Handle(GetCostCenterByIdQuery query, CancellationToken cancellationToken)
    {
        // get cost center by Id using dbContext
        // return result

        //Chwck companu
        var companyId = CompanyId.Of(query.CompanyId);
        var company = await dbContext.Companies.FindAsync([companyId], cancellationToken);
        if (company is null) throw EntityNotFoundException.For<Company>(query.CompanyId);

        //Chwck cost center
        var costCenterId = CostCenterId.Of(query.CostCenterId);
        var costCenter =
            await dbContext.CostCenters.FirstOrDefaultAsync(cc => cc.Id == costCenterId && cc.CompanyId == companyId,
                cancellationToken);

        if (costCenter is null) throw EntityNotFoundException.For<CostCenter>(query.CostCenterId);

        return new GetCostCenterByIdResult(costCenter.ToCostCenterDto());
    }
}