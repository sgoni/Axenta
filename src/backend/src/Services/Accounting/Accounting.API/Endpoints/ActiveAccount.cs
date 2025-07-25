namespace Accounting.API.Endpoints;

//- Accepts the account ID as a parameter.
//- Constructs a ActiveAccountCommand.
//- Sends the command using MediatR.
//- Returns a success or not found response.

//public record ActivateAccountRequest(Guid AccountId);

public record ActivateAccountResponse(bool IsSuccess);

public class ActiveAccount : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("/accounts/{id}/activate", async (Guid Id, ISender sender) =>
                {
                    var result = await sender.Send(new ActiveAccountCommand(Id));

                    var response = result.Adapt<ActivateAccountResponse>();

                    return Results.Ok(response);
                }
            )
            .WithName("ActivateAccount")
            .Produces<ActivateAccountResponse>()
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Activate an account.")
            .WithDescription("Activate an account.");
    }
}