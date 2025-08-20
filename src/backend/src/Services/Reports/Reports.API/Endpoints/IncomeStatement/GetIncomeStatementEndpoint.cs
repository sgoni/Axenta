namespace Reports.API.Endpoints.IncomeStatement;

//public record GetIncomeStatementRequest(Guid periodId, Guid companyId);
public record GetIncomeStatementResponse(IEnumerable<IncomeStatementDto> incomeStatementDto);

public class GetIncomeStatementEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/reports/income-statement",
                async (Guid periodId, Guid companyId, ISender sender) =>
                {
                    var result = await sender.Send(new GetIncomeStatementrQuery(periodId, companyId));

                    var response = result.Adapt<GetIncomeStatementResponse>();

                    return Results.Ok(response);
                })
            .WithName("GetIncomeStatement")
            .Produces<GetIncomeStatementResponse>()
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Only income and expenses accounts:")
            .WithDescription("Only income and expenses accounts:");
    }
}