namespace Accounting.API.Endpoints;

//public record GetAccountTypeRequest();
public record GetAccountTypesResponse(IEnumerable<AccountTypeDto> AccountTypes);

public class GetAccountTypes : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/accounts/types", async (ISender sender) =>
            {
                var result = await sender.Send(new GetAccountTypesQuery());

                var response = result.Adapt<GetAccountTypesResponse>();

                return Results.Ok(response);
            })
            .WithName("GetAccountTypes")
            .Produces<GetAccountTypesResponse>()
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .WithSummary("Get list of account types (asset, liability, etc.)")
            .WithDescription("Get list of account types (asset, liability, etc.)");
    }
}