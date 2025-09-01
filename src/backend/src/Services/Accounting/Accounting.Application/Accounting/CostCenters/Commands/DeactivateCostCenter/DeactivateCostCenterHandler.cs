namespace Accounting.Application.Accounting.CostCenters.Commands.DeactivateCostCenter;

public class DeactivateCostCenterHandler(IApplicationDbContext dbContext)
    : ICommandHandler<DeactivateCostCenterCommand, DeactiveCostCenterResult>
{
    public async Task<DeactiveCostCenterResult> Handle(DeactivateCostCenterCommand command,
        CancellationToken cancellationToken)
    {
        var costCenterId = CostCenterId.Of(command.CostCenterId);
        var costCenter = await dbContext.CostCenters.FindAsync([costCenterId], cancellationToken);

        if (costCenter is null) throw new AccountNotFoundException(command.CostCenterId);

        costCenter.Deactivate();
        await dbContext.SaveChangesAsync(cancellationToken);

        return new DeactiveCostCenterResult(true);
    }
}