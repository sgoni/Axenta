namespace Accounting.Application.Data;

public interface IApplicationDbContext
{
    DbSet<AccountType> AccountTypes { get; }
    DbSet<Account> Accounts { get; }
    DbSet<Period> Periods { get; }
    DbSet<JournalEntry> JournalEntries { get; }
    DbSet<JournalEntryLine> JournalEntryLines { get; }
    DbSet<DocumentReference> DocumentReferences { get; }
    DbSet<AuditLog> AuditLogs { get; }
    DbSet<CurrencyExchangeRate> CurrencyExchangeRates { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}