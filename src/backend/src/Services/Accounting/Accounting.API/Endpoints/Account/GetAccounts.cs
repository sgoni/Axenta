namespace Accounting.API.Endpoints.Account;

//- Accepts pagination parameters.
//- Constructs a GetAccountsQuery with these parameters.
//- Retrieves the data and returns it in a paginated format.

//public record GetAccountsRequest(PaginationRequest PaginationRequest);
public record GetAccountsResponse(PaginatedResult<AccountDto> Accounts);

public class GetAccounts : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/accounts", async ([AsParameters] PaginationRequest request, ISender sender) =>
            {
                var result = await sender.Send(new GetAccountsQuery(request));

                var response = result.Adapt<GetAccountsResponse>();

                return Results.Ok(response);
            })
            .WithName("GetAccounts")
            .Produces<GetAccountsResponse>()
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .WithSummary("Get list of accounting accounts")
            .WithDescription("Get list of accounting accounts");
    }
}