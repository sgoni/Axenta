namespace Accounting.Application.Accounting.CostCenters.Queries.GetCostCentersTree;

public record GetCostCentersTreeQuery(Guid CompanyId) : IQuery<GetCostCentersTreeResult>;

public record GetCostCentersTreeResult(IEnumerable<CostCenterTreeDto> CostCenterTree);