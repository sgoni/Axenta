namespace Accounting.Application.Dtos;

public record CurrencyExchangeRateDto(
    Guid Id,
    string CurrencyCode,
    DateOnly Date,
    decimal BuyRate,
    decimal SellRate
);