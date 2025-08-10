namespace Accounting.API.Endpoints.CurrencyExchangeRate;

//- Accepts pagination parameters.
//- Constructs a GetDailyCurrencyExchangeRateQuery with these parameters.
//- Retrieves the data and returns it in a paginated format.

//public record GetCurrencyExchangeRatesRequest(PaginationRequest PaginationRequest);

public record GetCurrencyExchangeRatesResponse(PaginatedResult<CurrencyExchangeRateDto> CurrencyExchangeRates);

public class GetCurrencyExchangeRates : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/currencies", async ([AsParameters] PaginationRequest request, ISender sender) =>
            {
                var result = await sender.Send(new GetCurrencyExchangeRatesQuery(request));

                var response = result.Adapt<GetCurrencyExchangeRatesResponse>();

                return Results.Ok(response);
            })
            .WithName("GetCurrencyExchangeRates")
            .Produces<GetCurrencyExchangeRatesResponse>()
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .WithSummary("List exchange rates")
            .WithDescription("List exchange rates");
    }
}