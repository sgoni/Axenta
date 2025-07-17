namespace Accounting.Infrastructure.Data.Extensions;

public static class DatabaseExtensions
{
    public static async Task InitialiseDatabaseAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();

        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        context.Database.MigrateAsync().GetAwaiter().GetResult();

        await SeedAsync(context);
    }

    private static async Task SeedAsync(ApplicationDbContext context)
    {
        await SeedAccountTypeAsync(context);
        await SeedAccountsAsync(context);
        await SeedPeriodsAsync(context);
        await SeedJornalEntryWithLinesAsync(context);
    }

    private static async Task SeedAccountTypeAsync(ApplicationDbContext context)
    {
        if (!await context.AccountTypes.AnyAsync())
        {
            await context.AccountTypes.AddRangeAsync(InitialData.AccountTypes);
            await context.SaveChangesAsync();
        }
    }

    private static async Task SeedAccountsAsync(ApplicationDbContext context)
    {
        if (!await context.AccountTypes.AnyAsync())
        {
            await context.Accounts.AddRangeAsync(InitialData.Accounts);
            await context.SaveChangesAsync();
        }
    }

    private static async Task SeedPeriodsAsync(ApplicationDbContext context)
    {
        if (!await context.AccountTypes.AnyAsync())
        {
            await context.Periods.AddRangeAsync(InitialData.Periods);
            await context.SaveChangesAsync();
        }
    }

    private static async Task SeedJornalEntryWithLinesAsync(ApplicationDbContext context)
    {
        if (!await context.JournalEntries.AnyAsync())
        {
            await context.JournalEntries.AddRangeAsync(InitialData.JournalEntriesWithLines);
            await context.SaveChangesAsync();
        }
    }
}