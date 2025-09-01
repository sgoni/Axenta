namespace Accounting.Application.Accounting.CostCenters.Commands.ActiveCostCenter;

public class ActiveCostCenterHandler(IApplicationDbContext dbContext)
    : ICommandHandler<ActiveCostCenterCommand, ActiveCostCenterResult>
{
    public async Task<ActiveCostCenterResult> Handle(ActiveCostCenterCommand command,
        CancellationToken cancellationToken)
    {
        var costCenterId = CostCenterId.Of(command.CostCenterId);
        var costCenter = await dbContext.CostCenters.FindAsync([costCenterId], cancellationToken);

        if (costCenter is null) throw new AccountNotFoundException(command.CostCenterId);

        costCenter.Activate();
        await dbContext.SaveChangesAsync(cancellationToken);

        return new ActiveCostCenterResult(true);
    }
}