namespace Accounting.Application.Dtos;

public record CostCenterDto(
    Guid Id,
    string Code,
    string Name,
    string? Description,
    bool IsActive,
    Guid CompanyId,
    Guid? ParentCostCenterId
);