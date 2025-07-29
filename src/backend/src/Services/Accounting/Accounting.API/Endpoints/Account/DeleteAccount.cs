namespace Accounting.API.Endpoints.Account;

//- Accepts the Account ID as a parameter.
//- Constructs a DeleteAccountCommand.
//- Sends the command using MediatR.
//- Returns a success or not found response.

//public record DeleteAccountRequest(Guid AccountId);

public record DeleteAccountResponse(bool IsSuccess);

public class DeleteAccount : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/accounts/{id}", async (Guid Id, ISender sender) =>
                {
                    var result = await sender.Send(new DeleteAccountCommand(Id));

                    var response = result.Adapt<DeleteAccountResponse>();

                    return Results.Ok(response);
                }
            )
            .WithName("DeleteAccount")
            .Produces<DeleteAccountResponse>()
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .WithSummary("Delete (or inactivate) an account.")
            .WithDescription("Delete (or inactivate) an account.");
    }
}