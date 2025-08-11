namespace Accounting.API.Endpoints.Company;

//- Accepts a UpdateCompanyRequest.
//- Maps the request to an UpdateAccountCommand.
//- Sends the command for processing.
//- Returns a success or error response based on the outcome.

public record UpdateCompanyRequest(CompanyDto Company);

public record UpdateCompanyResponse(bool IsSuccess);

public class UpdateCompany : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("/companies", async (UpdateCompanyRequest request, ISender sender) =>
            {
                var command = request.Adapt<UpdateCompanyCommand>();

                var result = await sender.Send(command);

                var response = result.Adapt<UpdateCompanyResponse>();

                return Results.Ok(response);
            })
            .WithName("UpdateCompany")
            .Produces<UpdateCompanyResponse>()
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Update an existing company")
            .WithDescription("Update an existing company");
    }
}