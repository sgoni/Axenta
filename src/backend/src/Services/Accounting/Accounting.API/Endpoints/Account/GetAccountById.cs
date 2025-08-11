namespace Accounting.API.Endpoints.Account;

//- Accepts a customer ID.
//- Uses a GetOrdersByCustomerQuery to fetch orders.
//- Returns the list of orders for that customer.m

//public record GetAccountByIdRequest(Guid AccountId);

public record GetAccountByIdResponse(AccountDto AccountDetail);

public class GetAccountById : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/accounts/{accountId}", async (Guid accountId, ISender sender) =>
            {
                var result = await sender.Send(new GetAccountByIdQuery(accountId));

                var response = result.Adapt<GetAccountByIdResponse>();

                return Results.Ok(response);
            })
            .WithName("GetAccountById")
            .Produces<GetAccountByIdResponse>()
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .WithSummary("Get a specific account.")
            .WithDescription("Get a specific account.");
    }
}