namespace Accounting.API.Endpoints;

public record UpdateAccountRequest(AccountDto AccountDetail);

public record UpdateAccountResponse(bool IsSuccescs);

public class UpdateAccount : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("/accounts", async (UpdateAccountRequest request, ISender sender) =>
            {
                var command = request.Adapt<UpdateAccountCommand>();

                var result = await sender.Send(command);

                var response = result.Adapt<UpdateAccountResponse>();

                return Results.Ok(response);
            })
            .WithName("UpdateAccount")
            .Produces<UpdateAccountResponse>()
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Update an existing account")
            .WithDescription("Update an existing account");
        ;
    }
}