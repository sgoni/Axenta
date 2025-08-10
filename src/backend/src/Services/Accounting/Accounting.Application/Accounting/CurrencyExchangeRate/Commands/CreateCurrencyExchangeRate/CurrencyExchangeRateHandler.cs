namespace Accounting.Application.Accounting.CurrencyExchangeRate.Commands.CreateCurrencyExchangeRate;

public class CurrencyExchangeRateHandler(IApplicationDbContext dbContext)
    : ICommandHandler<CurrencyExchangeRateCommand, CurrencyExchangeRateResult>
{
    public async Task<CurrencyExchangeRateResult> Handle(CurrencyExchangeRateCommand command,
        CancellationToken cancellationToken)
    {
        //create CurrencyExchangeRate Entry entity from command object
        //save to database
        //return result

        var currencyExchangeRate = CreateNewCurrencyExchangeRate(command.CurrencyExchangeRate);
        dbContext.CurrencyExchangeRates.Add(currencyExchangeRate);
        await dbContext.SaveChangesAsync(cancellationToken);

        return new CurrencyExchangeRateResult(currencyExchangeRate.Id.Value);
    }

    private Domain.Models.CurrencyExchangeRate CreateNewCurrencyExchangeRate(
        CurrencyExchangeRateDto currencyExchangeRateDto)
    {
        var currencyExchangeRate = Domain.Models.CurrencyExchangeRate.Create(
            CurrencyExchangeRateId.Of(Guid.NewGuid()),
            currencyExchangeRateDto.CurrencyCode,
            currencyExchangeRateDto.Date,
            currencyExchangeRateDto.BuyRate,
            currencyExchangeRateDto.SellRate
        );

        return currencyExchangeRate;
    }
}