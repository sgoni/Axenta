namespace Accounting.Application.Dtos;

public record CostCenterTreeDto(
    Guid Id,
    string Code,
    string Name,
    Guid? ParentCostCenterId,
    List<CostCenterTreeDto> Children);