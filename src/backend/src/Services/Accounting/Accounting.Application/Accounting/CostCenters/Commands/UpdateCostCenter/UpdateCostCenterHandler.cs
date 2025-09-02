namespace Accounting.Application.Accounting.CostCenters.Commands.UpdateCostCenter;

public class UpdateCostCenterHandler(IApplicationDbContext dbContext)
    : ICommandHandler<UpdateCostCenterCommand, UpdateCostCenterResult>
{
    public async Task<UpdateCostCenterResult> Handle(UpdateCostCenterCommand command,
        CancellationToken cancellationToken)
    {
        //Update cost center entity from command object
        //save to database
        //return result

        var costCenterId = CostCenterId.Of(command.CostCenter.Id);
        var costCenter = await dbContext.CostCenters.FindAsync([costCenterId], cancellationToken);

        if (costCenter is null) throw EntityNotFoundException.For<CostCenter>(command.CostCenter.Id);

        UpdateCostCenterWithNewValues(costCenter, command.CostCenter);
        await dbContext.SaveChangesAsync(cancellationToken);

        return new UpdateCostCenterResult(true);
    }

    private void UpdateCostCenterWithNewValues(CostCenter costCenter, CostCenterDto commandCostCenter)
    {
        costCenter.Update(
            commandCostCenter.Name,
            commandCostCenter.Description,
            commandCostCenter.IsActive,
            CostCenterId.FromNullable(commandCostCenter.ParentCostCenterId),
            costCenter
        );
    }
}