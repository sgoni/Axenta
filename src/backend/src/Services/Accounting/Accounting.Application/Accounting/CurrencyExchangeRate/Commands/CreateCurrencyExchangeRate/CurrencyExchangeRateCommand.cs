namespace Accounting.Application.Accounting.CurrencyExchangeRate.Commands.CreateCurrencyExchangeRate;

public record CurrencyExchangeRateCommand(CurrencyExchangeRateDto CurrencyExchangeRate)
    : ICommand<CurrencyExchangeRateResult>;

public record CurrencyExchangeRateResult(Guid id);