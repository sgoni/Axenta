namespace Accounting.Application.Accounting.Periods.Commands.CreatePeriod;

public class CreatePeriodHandler(IApplicationDbContext dbContext)
    : ICommandHandler<CreatePeriodCommand, CreatePeriodResult>
{
    public async Task<CreatePeriodResult> Handle(CreatePeriodCommand command, CancellationToken cancellationToken)
    {
        //Check not to duplicate period
        //Create period entity from command object
        //Save to database
        //return result

        var exists = await dbContext.Periods
            .AnyAsync(p => p.Year == DateTime.Now.Year && p.Month == DateTime.Now.Month);

        if (exists)
            throw new ConflictException("The period already exists.");

        var period = CreateNewPeriod();
        dbContext.Periods.Add(period);
        await dbContext.SaveChangesAsync(cancellationToken);

        return new CreatePeriodResult(period.Id.Value);
    }

    private Period CreateNewPeriod()
    {
        var newPeriod =
            Period.Create(PeriodId.Of(Guid.NewGuid()),
                DateTime.Now.Year,
                DateTime.Now.Month
            );

        return newPeriod;
    }
}