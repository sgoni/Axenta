namespace Accounting.API.Endpoints.CurrencyExchangeRate;

//public record GetDailyCurrencyExchangeRatesRequest();

public record GetDailyCurrencyExchangeRatesResponse(CurrencyExchangeRateDto CurrencyExchangeRate);

public class GetDailyCurrencyExchangeRate : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/currencies/daily", async (ISender sender) =>
            {
                var result = await sender.Send(new GetDailyCurrencyExchangeRateQuery());

                var response = result.Adapt<GetDailyCurrencyExchangeRatesResponse>();

                return Results.Ok(response);
            })
            .WithName("GetDailyCurrencyExchangeRate")
            .Produces<GetDailyCurrencyExchangeRatesResponse>()
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .WithSummary("Get the daily currency exchange rate")
            .WithDescription("Get the daily currency exchange rate");
    }
}