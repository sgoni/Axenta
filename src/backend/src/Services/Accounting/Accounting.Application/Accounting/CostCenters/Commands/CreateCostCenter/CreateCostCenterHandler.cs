namespace Accounting.Application.Accounting.CostCenters.Commands.CreateCostCenter;

public class CreateCostCenterHandler(IApplicationDbContext dbContext)
    : ICommandHandler<CreateCostCenterCommand, CreateCostCenterResult>
{
    public async Task<CreateCostCenterResult> Handle(CreateCostCenterCommand command,
        CancellationToken cancellationToken)
    {
        //Create CostCenter entity from command object
        //Save to database
        //return result
        var newCode = string.Empty;
        CostCenter? parentCostCenter = null;

        if (command.CostCenter.ParentCostCenterId != null)
        {
            var parentCostCentertId = CostCenterId.Of(command.CostCenter.ParentCostCenterId.Value);
            parentCostCenter = await dbContext.CostCenters.FindAsync(parentCostCentertId);

            if (parentCostCenter is null)
                throw new Exception("Parent Cost Center does not exist.");
            // Find out how many children that father has
            var siblingCount = await dbContext.CostCenters.CountAsync(a => a.ParentCostCenterId == parentCostCenter.Id);
            // Eg: If there are two children, the next one will be number 3
            var suffix = (siblingCount + 1).ToString("D2"); // '01', '02', etc
            newCode = $"{parentCostCenter.Code}.{suffix}";
        }
        else
        {
            // Root account: generates its own unique root code
            var rootCostCenter = await dbContext.CostCenters.CountAsync(a => a.ParentCostCenterId == null);
            newCode = (rootCostCenter + 1).ToString(); // '1', '2'. '3', etc.
        }

        var costCenter = CreateNewCostCenter(command.CostCenter, parentCostCenter, newCode);
        dbContext.CostCenters.Add(costCenter);
        await dbContext.SaveChangesAsync(cancellationToken);

        return new CreateCostCenterResult(costCenter.Id.Value);
    }

    private CostCenter CreateNewCostCenter(CostCenterDto costCenterDto, CostCenter? parentCostCenter, string newCode)
    {
        var newCostCenter = CostCenter.Create(
            CostCenterId.Of(Guid.NewGuid()),
            newCode,
            costCenterDto.Name,
            costCenterDto.Description,
            CompanyId.Of(costCenterDto.CompanyId),
            parentCostCenter?.Id
        );

        return newCostCenter;
    }
}