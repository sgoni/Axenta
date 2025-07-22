namespace Accounting.Application.Dtos;

public record AccountDetailDto(
    Guid Id,
    string Code,
    string Name,
    Guid AccountTypeId,
    Guid ParentId,
    bool IsActive
);