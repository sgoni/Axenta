namespace Axenta.Reports.API.Endpoints.GetBalanceSheet;

//public record GetBalanceSheetRequest(Guid periodId, Guid companyId);
public record GetBalanceSheetResponse(IEnumerable<BalanceSheetDto> balanceSheet);

public class GetBalanceSheetEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/reports/balance-sheet",
                async (Guid periodId, Guid companyId, ISender sender) =>
                {
                    var result = await sender.Send(new GetBalanceSheetQuery(periodId, companyId));

                    var response = result.Adapt<GetBalanceSheetResponse>();

                    return Results.Ok(response);
                })
            .WithName("GetBalanceSheet")
            .Produces<GetBalanceSheetResponse>()
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Get Balance Sheet")
            .WithDescription("Get Balance Sheet");
    }
}