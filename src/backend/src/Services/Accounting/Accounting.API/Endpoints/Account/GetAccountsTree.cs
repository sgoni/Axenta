namespace Accounting.API.Endpoints.Account;

//- Accepts pagination parameters.
//- Constructs a GetAccountsQuery with these parameters.
//- Retrieves the data and returns it in a paginated format.

//public record GetAccountsTreeRequest(PaginationRequest PaginationRequest);
public record GetAccountsTreeResponse(IEnumerable<AccountTreeDto> AccountTree);

public class GetAccountsTree : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/accounts/tree", async (ISender sender) =>
            {
                var result = await sender.Send(new GetAccountsTreeQuery());

                var response = result.Adapt<GetAccountsTreeResponse>();

                return Results.Ok(response);
            })
            .WithName("GetAccountsTree")
            .Produces<GetAccountsTreeResponse>()
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .WithSummary("Obtain the hierarchical tree of accounts")
            .WithDescription("Obtain the hierarchical tree of accounts");
    }
}