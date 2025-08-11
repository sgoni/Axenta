namespace Accounting.API.Endpoints.Company;

//- Accepts pagination parameters.
//- Constructs a GetAccountsQuery with these parameters.
//- Retrieves the data and returns it in a paginated format.

//public record GetCompaniesRequest(PaginationRequest PaginationRequest);
public record GetCompaniesResponse(PaginatedResult<CompanyDto> Companies);

public class GetCompanies : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/companies", async ([AsParameters] PaginationRequest request, ISender sender) =>
            {
                var result = await sender.Send(new GetCompaniesQuery(request));

                var response = result.Adapt<GetCompaniesResponse>();

                return Results.Ok(response);
            })
            .WithName("GetCompanies")
            .Produces<GetCompaniesResponse>()
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .WithSummary("Get list of companies")
            .WithDescription("Get list of companies");
    }
}