namespace Reports.API.Endpoints.GetAccountBalanceByPeriod;

//public record GetAccountBalanceByPeriodRequest(Guid periodId);
public record GetAccountBalanceByPeriodResponse(decimal Balance);

public class GetAccountBalanceByPeriodEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/reports/account-balance/period",
                async (Guid periodId, Guid accountId, ISender sender) =>
                {
                    var result = await sender.Send(new GetAccountBalanceByPeriodQuery(periodId, accountId));

                    var response = result.Adapt<GetAccountBalanceByPeriodResponse>();

                    return Results.Ok(response);
                })
            .WithName("GetAccountBalanceByPeriod")
            .Produces<GetAccountBalanceByPeriodResponse>()
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Get the balance on account and period")
            .WithDescription("Get the balance on account and period");
    }
}