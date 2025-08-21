namespace Reports.API.Dtos;

public record TrialBalanceDto(
    string Code,
    string Name,
    decimal TotalDebit,
    decimal TotalCredit,
    decimal Balance);