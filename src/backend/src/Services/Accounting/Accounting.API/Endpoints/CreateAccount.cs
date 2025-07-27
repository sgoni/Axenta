namespace Accounting.API.Endpoints;

public record CreateAccountRequest(AccountDto Account);

public record CreateAccountResponse(Guid Id);

public class CreateAccount : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/accounts", async (CreateAccountRequest request, ISender sender) =>
                {
                    var command = request.Adapt<CreateAccountCommand>();

                    var result = await sender.Send(command);

                    var response = result.Adapt<CreateAccountResponse>();

                    return Results.Created($"/accounts/{response.Id}", response);
                }
            )
            .WithName("CreateAccount")
            .Produces<CreateAccountResponse>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Create a new account")
            .WithDescription("Create a new account");
    }
}