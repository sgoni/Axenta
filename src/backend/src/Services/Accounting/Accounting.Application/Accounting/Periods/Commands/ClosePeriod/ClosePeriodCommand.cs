namespace Accounting.Application.Accounting.Periods.Commands.ClosePeriod;

public record ClosePeriodCommand(Guid PeriodId) : ICommand<ClosePeriodResult>;

public record ClosePeriodResult(bool IsSuccess);