namespace Accounting.API.Endpoints.Auditlogs;

//- Accepts a AuditLog ID.
//- Uses a GetAuditLogByIdQuery to fetch orders.
//- Returns AuditLog for thar Id.

//public record GetAuditLogByIdRequest(Guid PeriodId);

public record GetAuditLogByIdResponse(AuditLogDto AuditLogDetail);

public class GetAuditLogById : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/audit-logs/{id}", async (Guid id, ISender sender) =>
            {
                var result = await sender.Send(new GetAuditLogByIdQuery(id));

                var response = result.Adapt<GetAuditLogByIdResponse>();

                return Results.Ok(response);
            })
            .WithName("GetAuditLogById")
            .Produces<GetAuditLogByIdResponse>()
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .WithSummary("View details of a specific change.")
            .WithDescription("View details of a specific change.");
    }
}