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

        //Exists company
        var companyId = CompanyId.Of(command.Period.CompanyId);
        var company = await dbContext.Companies.FindAsync(companyId, cancellationToken);

        if (company is null)
            throw new CompanyNotFoundException(command.Period.CompanyId);

        var exists = await dbContext.Periods
            .AnyAsync(p => p.Year == DateTime.Now.Year && p.Month == DateTime.Now.Month);

        if (exists)
            throw new ConflictException("The period already exists.");

        var period = CreateNewPeriod(command.Period.CompanyId);
        dbContext.Periods.Add(period);
        await dbContext.SaveChangesAsync(cancellationToken);

        return new CreatePeriodResult(period.Id.Value);
    }

    private Period CreateNewPeriod(Guid companyId = default)
    {
        var newPeriod =
            Period.Create(CompanyId.Of(companyId),
                PeriodId.Of(Guid.NewGuid()),
                DateTime.Now.Year,
                DateTime.Now.Month
            );

        return newPeriod;
    }
}