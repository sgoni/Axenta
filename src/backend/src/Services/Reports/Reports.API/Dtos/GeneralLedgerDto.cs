namespace Reports.API.Dtos;

public record GeneralLedgerDto(
    DateTime Date,
    string Description,
    decimal Debit,
    decimal Credit,
    decimal Movement
);