namespace Accounting.Application.Accounting.Periods.Commands.ClosePeriod;

public record ClosePeriodCommand(Guid PeriodId) : ICommand<ClosePeriodResult>;

public record ClosePeriodResult(bool IsSuccess);

public class ClosePeriodCommandValidator : AbstractValidator<ClosePeriodCommand>
{
    public ClosePeriodCommandValidator()
    {
        RuleFor(x => x.PeriodId).NotEmpty().WithMessage("PeriodId is required");
    }
}