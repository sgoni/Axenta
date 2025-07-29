namespace Accounting.API.Endpoints.Auditlogs;

//- Accepts pagination parameters.
//- Constructs a GetAuditlogsQuery with these parameters.
//- Retrieves the data and returns it in a paginated format.

//public record GetAuditlogsRequest(PaginationRequest PaginationRequest);

public record GetAuditlogsResponse(PaginatedResult<AuditLogDto> Auditlogs);

public class GetAuditLogsLIcar : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/audit-logs", async ([AsParameters] PaginationRequest request, ISender sender) =>
            {
                var result = await sender.Send(new GetAuditLogsQuery(request));

                var response = result.Adapt<GetAuditlogsResponse>();

                return Results.Ok(response);
            })
            .WithName("GetAuditlogs")
            .Produces<GetAuditlogsResponse>()
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .WithSummary("Consult audit logs")
            .WithDescription("Consult audit logs");
    }
}