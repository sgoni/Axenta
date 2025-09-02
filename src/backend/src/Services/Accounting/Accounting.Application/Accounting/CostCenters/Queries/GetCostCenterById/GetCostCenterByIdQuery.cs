namespace Accounting.Application.Accounting.CostCenters.Queries.GetCostCenterById;

public record GetCostCenterByIdQuery(Guid CostCenterId, Guid CompanyId) : IQuery<GetCostCenterByIdResult>;

public record GetCostCenterByIdResult(CostCenterDto CostCenterDetail);