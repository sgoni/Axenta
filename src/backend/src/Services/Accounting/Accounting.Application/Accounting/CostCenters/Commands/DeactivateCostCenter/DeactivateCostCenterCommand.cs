namespace Accounting.Application.Accounting.CostCenters.Commands.DeactivateCostCenter;

public record DeactivateCostCenterCommand(Guid CostCenterId) : ICommand<DeactiveCostCenterResult>;

public record DeactiveCostCenterResult(bool IsSuccess);

public class DeactivateCostCenterCommandValidator : AbstractValidator<DeactivateCostCenterCommand>
{
    public DeactivateCostCenterCommandValidator()
    {
        RuleFor(x => x.CostCenterId).NotEmpty().WithMessage("CostCenterId is required");
    }
}