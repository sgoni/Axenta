namespace Reports.API.Endpoints.GetAccountBalance;

//public record GetAccountBalanceRequest(Guid AccountId), Guid companyId;
public record GetAccountBalanceResponse(decimal Balance);

public class GetAccountBalanceEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/reports/account-balance/{accountId}/{companyId}",
                async (Guid accountId, Guid companyId, ISender sender) =>
                {
                    var result = await sender.Send(new GetAccountBalanceQuery(accountId, companyId));

                    var response = result.Adapt<GetAccountBalanceResponse>();

                    return Results.Ok(response);
                })
            .WithName("GetAccountBalance")
            .Produces<GetAccountBalanceResponse>()
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Get the balance of a specific account")
            .WithDescription("Get the balance of a specific account");
    }
}