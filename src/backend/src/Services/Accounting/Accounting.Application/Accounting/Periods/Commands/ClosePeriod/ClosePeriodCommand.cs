namespace Accounting.Application.Accounting.Periods.Commands.ClosePeriod;

public record ClosePeriodCommand(ClosePeriodDto ClosePeriod) : ICommand<ClosePeriodResult>;

public record ClosePeriodResult(bool IsSuccess);

public class ClosePeriodCommandValidator : AbstractValidator<ClosePeriodCommand>
{
    public ClosePeriodCommandValidator()
    {
        RuleFor(x => x.ClosePeriod.CompanyId).NotEmpty().WithMessage("CompanyId is required");
        RuleFor(x => x.ClosePeriod.PeriodId).NotEmpty().WithMessage("PeriodId is required");
    }
}