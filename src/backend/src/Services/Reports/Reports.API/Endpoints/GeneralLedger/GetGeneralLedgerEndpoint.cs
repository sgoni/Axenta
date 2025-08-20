namespace Reports.API.Endpoints.GeneralLedger;

//public record GetBalanceSheetRequest(Guid periodId, Guid companyId, Guid AccountId);
public record GetGeneralLedgerResponse(IEnumerable<GeneralLedgerDto> generalLedgerDto);

public class GetGeneralLedgerEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/reports/general-ledger",
                async (Guid periodId, Guid companyId, Guid accountID, ISender sender) =>
                {
                    var result = await sender.Send(new GetGeneralLedgerQuery(periodId, companyId, accountID));

                    var response = result.Adapt<GetGeneralLedgerResponse>();

                    return Results.Ok(response);
                })
            .WithName("GetGeneralLedger")
            .Produces<GetGeneralLedgerResponse>()
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Detailed movements of an account:")
            .WithDescription("Detailed movements of an account:");
    }
}