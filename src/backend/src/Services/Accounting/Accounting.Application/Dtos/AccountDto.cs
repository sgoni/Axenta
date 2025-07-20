namespace Accounting.Application.Dtos;

public record AccountDto(
    Guid Id,
    string Code,
    string Name,
    Guid AccountTypeId,
    Guid ParentId,
    bool IsActive,
    Guid Type
);