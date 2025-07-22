namespace Accounting.Application.Dtos;

public class AccountDto(
    Guid Id,
    string Code,
    string Name,
    Guid AccountTypeId,
    Guid ParentAccountId,
    bool IsActive
);