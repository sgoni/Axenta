namespace Accounting.Application.Dtos;

public record AccountTreeDto(Guid Id, string Code, string Name, Guid AccountType, AccountTreeDto[] Children);