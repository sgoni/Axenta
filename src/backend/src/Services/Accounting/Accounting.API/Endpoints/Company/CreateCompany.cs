namespace Accounting.API.Endpoints.Company;

//- Accepts a CreatCompanyRequest object.
//- Maps the request to a CreateAccountCommand.
//- Uses MediatR to send the command to the corresponding handler.
//- Returns a response with the created account's ID.

public record CreatCompanyRequest(CompanyDto Company);

public record CreateCompanyResponse(Guid Id);

public class CreateCompany : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/companies", async (CreatCompanyRequest request, ISender sender) =>
                {
                    var command = request.Adapt<CreateCompanyCommand>();

                    var result = await sender.Send(command);

                    var response = result.Adapt<CreateCompanyResponse>();

                    return Results.Created($"/companies/{response.Id}", response);
                }
            )
            .WithName("CreateCompany")
            .Produces<CreateCompanyResponse>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Create a new company")
            .WithDescription("Create a new company");
    }
}