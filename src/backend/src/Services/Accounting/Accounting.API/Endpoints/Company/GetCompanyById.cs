using Accounting.Application.Accounting.Companies.Queries.GetCompanyById;

namespace Accounting.API.Endpoints.Company;

//- Accepts a customer ID.
//- Uses a GetOrdersByCustomerQuery to fetch orders.
//- Returns the list of orders for that customer.m

//public record GetCompanyByIdRequest(Guid CompanyId);

public record GetCompanyByIdResponse(CompanyDto CompanyDetail);

public class GetCompanyById : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/companies/{companyId}", async (Guid companyId, ISender sender) =>
            {
                var result = await sender.Send(new GetCompanyByIdQuery(companyId));

                var response = result.Adapt<GetCompanyByIdResponse>();

                return Results.Ok(response);
            })
            .WithName("GetCompanyById")
            .Produces<GetCompanyByIdResponse>()
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .WithSummary("Get a specific company.")
            .WithDescription("Get a specific company.");
    }
}