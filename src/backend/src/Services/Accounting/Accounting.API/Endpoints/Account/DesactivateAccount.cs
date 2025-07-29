namespace Accounting.API.Endpoints.Account;

//- Accepts the account ID as a parameter.
//- Constructs a DeactivateAccountCommand.
//- Sends the command using MediatR.
//- Returns a success or not found response.

//public record DesactivateAccountRequest(Guid AccountId);

public record DesactivateAccountResponse(bool IsSuccess);

public class DesactivateAccount : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("/accounts/{id}/desactivate", async (Guid Id, ISender sender) =>
                {
                    var result = await sender.Send(new DeactivateAccountCommand(Id));

                    var response = result.Adapt<DesactivateAccountResponse>();

                    return Results.Ok(response);
                }
            )
            .WithName("DesactivateAccount")
            .Produces<DesactivateAccountResponse>()
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Desactivate an account.")
            .WithDescription("Desactivate an account.");
    }
}