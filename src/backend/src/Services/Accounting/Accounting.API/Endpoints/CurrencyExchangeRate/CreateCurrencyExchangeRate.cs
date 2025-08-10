namespace Accounting.API.Endpoints.CurrencyExchangeRate;

//- Accepts a CreateCurrencyExchangeRate object.
//- Maps the request to a CreateDocumentReferenceCommand.
//- Uses MediatR to send the command to the corresponding handler.
//- Returns a response with the created account's ID.

public record CreateCurrencyExchangeRateRequest(DocumentReferenceDto DocumentReference);

public record CreateCurrencyExchangeRateResponse(Guid Id);

public class CreateCurrencyExchangeRate : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("currency", async (CreateCurrencyExchangeRateRequest request, ISender sender) =>
            {
                var command = request.Adapt<CreateDocumentReferenceCommand>();

                var result = await sender.Send(command);

                var response = result.Adapt<CreateCurrencyExchangeRateResponse>();

                return Results.Created($"/currency/{response.Id}", response);
            })
            .WithName("CreateCurrencyExchangeRate")
            .Produces<CreateCurrencyExchangeRateResponse>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Create a new exchange rate")
            .WithDescription("Create a new exchange rate");
    }
}