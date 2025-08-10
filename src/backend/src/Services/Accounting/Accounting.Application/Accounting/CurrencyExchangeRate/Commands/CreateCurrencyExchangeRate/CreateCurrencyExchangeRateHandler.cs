namespace Accounting.Application.Accounting.CurrencyExchangeRate.Commands.CreateCurrencyExchangeRate;

public class CreateCurrencyExchangeRateHandler(IApplicationDbContext dbContext)
    : ICommandHandler<CreateCurrencyExchangeRateCommand, CreateCurrencyExchangeRateResult>
{
    public async Task<CreateCurrencyExchangeRateResult> Handle(CreateCurrencyExchangeRateCommand command,
        CancellationToken cancellationToken)
    {
        //create CurrencyExchangeRate Entry entity from command object
        //save to database
        //return result

        var currencyExchangeRate = CreateNewCurrencyExchangeRate(command.CurrencyExchangeRate);
        dbContext.CurrencyExchangeRates.Add(currencyExchangeRate);
        await dbContext.SaveChangesAsync(cancellationToken);

        return new CreateCurrencyExchangeRateResult(currencyExchangeRate.Id.Value);
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