namespace Reports.API.Dtos;

public record BalanceSheetDto(
    string Code,
    string Name,
    string AccountType,
    decimal Balance
);