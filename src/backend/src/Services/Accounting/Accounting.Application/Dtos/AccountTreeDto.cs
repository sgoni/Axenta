namespace Accounting.Application.Dtos;

public record AccountTreeDto(
    Guid Id,
    string Code,
    string Name,
    Guid AccountTypeId,
    Guid? ParentAccountId,
    List<AccountTreeDto> Children);