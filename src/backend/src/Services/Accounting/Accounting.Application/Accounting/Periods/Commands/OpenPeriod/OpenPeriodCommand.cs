namespace Accounting.Application.Accounting.Periods.Commands.OpenPeriod;

public record OpenPeriodCommand(Guid PeriodId) : ICommand<OpenPeriodResult>;

public record OpenPeriodResult(bool IsSuccess);