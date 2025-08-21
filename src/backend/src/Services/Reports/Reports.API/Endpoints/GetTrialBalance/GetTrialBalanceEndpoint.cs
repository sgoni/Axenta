namespace Reports.API.Endpoints.GetTrialBalance;

//public record GetTrialBalancRequest(Guid periodId, Guid companyId);
public record GetTrialBalanceResponse(IEnumerable<TrialBalanceDto> trialBalanceDto);

public class GetTrialBalanceEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/reports/trial-balance",
                async (Guid periodId, Guid companyId, ISender sender) =>
                {
                    var result = await sender.Send(new GetTrialBalanceQuery(periodId, companyId));

                    var response = result.Adapt<GetTrialBalanceResponse>();

                    return Results.Ok(response);
                })
            .WithName("GetTrialBalance")
            .Produces<GetTrialBalanceResponse>()
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Basic consultation that adds debit and credits on behalf of a period:t")
            .WithDescription("Basic consultation that adds debit and credits on behalf of a period:");
    }
}