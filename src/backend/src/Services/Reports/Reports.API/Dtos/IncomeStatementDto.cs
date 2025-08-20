namespace Reports.API.Dtos;

public record IncomeStatementDto(
    string Code,
    string Name,
    string AccountType,
    decimal Balance);