namespace Accounting.Application.Accounting.CostCenters.Commands.ActiveCostCenter;

public record ActiveCostCenterCommand(Guid CostCenterId) : ICommand<ActiveCostCenterResult>;

public record ActiveCostCenterResult(bool IsSuccess);

public class ActivateCostCenterCommandValidator : AbstractValidator<ActiveCostCenterCommand>
{
    public ActivateCostCenterCommandValidator()
    {
        RuleFor(x => x.CostCenterId).NotEmpty().WithMessage("CostCenterId is required");
    }
}