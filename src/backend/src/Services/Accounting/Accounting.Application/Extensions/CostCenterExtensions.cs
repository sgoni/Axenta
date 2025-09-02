namespace Accounting.Application.Extensions;

public static class CostCenterExtensions
{
    public static IEnumerable<CostCenterDto> ToCostCenterDtoList(this IEnumerable<CostCenter> costCenters)
    {
        return costCenters.Select(costCenter => new CostCenterDto(
            costCenter.Id.Value,
            costCenter.Code,
            costCenter.Name,
            costCenter.Description,
            costCenter.IsActive,
            costCenter.CompanyId.Value,
            costCenter.ParentCostCenterId?.Value
        ));
    }

    public static CostCenterDto ToCostCenterDto(this CostCenter costCenter)
    {
        return DtoFromCostCenter(costCenter);
    }

    private static CostCenterDto DtoFromCostCenter(CostCenter costCenter)
    {
        return new CostCenterDto(
            costCenter.Id.Value,
            costCenter.Code,
            costCenter.Name,
            costCenter.Description,
            costCenter.IsActive,
            costCenter.CompanyId.Value,
            costCenter.ParentCostCenterId?.Value
        );
    }

    public static List<CostCenterTreeDto>? ToCostCenterTreeDtoList(this IEnumerable<CostCenter> costCenters)
    {
        return new List<CostCenterTreeDto>(costCenters.Select(costCenter => new CostCenterTreeDto(
            costCenter.Id.Value,
            costCenter.Code,
            costCenter.Name,
            costCenter.ParentCostCenterId?.Value,
            new List<CostCenterTreeDto>()
        )));
    }

    public static List<CostCenterTreeDto> BuildTree(List<CostCenterTreeDto>? flatCostCenters)
    {
        var lookup = flatCostCenters.ToDictionary(x => x.Id);
        var roots = new List<CostCenterTreeDto>();

        foreach (var costCenter in flatCostCenters)
            if (costCenter.ParentCostCenterId == null)
                roots.Add(costCenter);
            else if (lookup.TryGetValue(costCenter.ParentCostCenterId.Value, out var parent))
                parent.Children.Add(costCenter);

        return roots;
    }
}