namespace Accounting.Application.Accounting.CurrencyExchangeRate.Commands.CreateCurrencyExchangeRate;

public record CreateCurrencyExchangeRateCommand(CurrencyExchangeRateDto CurrencyExchangeRate)
    : ICommand<CreateCurrencyExchangeRateResult>;

public record CreateCurrencyExchangeRateResult(Guid Id);

public class CurrencyExchangeRateValidator : AbstractValidator<CreateCurrencyExchangeRateCommand>
{
    public CurrencyExchangeRateValidator()
    {
        RuleFor(x => x.CurrencyExchangeRate.Id).NotEmpty().WithMessage("Id is required");
        RuleFor(x => x.CurrencyExchangeRate.CurrencyCode).NotNull().MaximumLength(3)
            .WithMessage("CurrencyCode must be a 3-letter ISO code and is required");
        RuleFor(x => x.CurrencyExchangeRate.Date).NotEmpty().WithMessage("Date is required.");
        RuleFor(x => x.CurrencyExchangeRate.BuyRate).GreaterThanOrEqualTo(1)
            .WithMessage("Purchase rate must be greater than zero.");
        RuleFor(x => x.CurrencyExchangeRate.SellRate).GreaterThanOrEqualTo(1)
            .WithMessage("Sales rate must be greater than zero..");
    }
}