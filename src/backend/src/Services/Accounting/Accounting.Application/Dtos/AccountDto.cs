namespace Accounting.Application.Dtos;

public record AccountDto(
    Guid Id,
    string Code,
    string Name,
    Guid AccountTypeId,
    Guid? ParentAccountId,
    bool IsActive,
    int Level,
    bool IsMovable
);