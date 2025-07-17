namespace Accounting.Infrastructure.Data.Extensions;

public class InitialData
{
    public static IEnumerable<AccountType> AccountTypes =>
        new List<AccountType>
        {
            AccountType.Create(AccountTypeId.Of(new Guid("9546c264-0869-44d9-8031-371159b76d3f")), name: "Activo",
                description: "Cuentas de activo"),
            AccountType.Create(AccountTypeId.Of(new Guid("d508e0ce-20d8-4e91-9e62-7a5d122aad2d")), name: "Pasivo",
                description: "Cuentas de pasivo"),
            AccountType.Create(AccountTypeId.Of(new Guid("77084a67-ab6c-4a75-b673-2094ad375c0b")), name: "Capital",
                description: "Cuentas de capital"),
            AccountType.Create(AccountTypeId.Of(new Guid("6e705d5f-2d5c-449e-a187-17dc00d2c914")), name: "Ingreso",
                description: "Cuentas de ingresos"),
            AccountType.Create(AccountTypeId.Of(new Guid("71bbd6e0-abf4-4f2a-afec-9199bb404b08")), name: "Gasto",
                description: "Cuentas de gastos"),
        };

    public static IEnumerable<Account> Accounts =>
        new List<Account>
        {
            Account.Create(AccountId.Of(new Guid("7573e77c-110b-4a3b-9bda-7be306bb14b4")), "1001", name: "Caja",
                accountTypeId: AccountTypeId.Of(new Guid("9546c264-0869-44d9-8031-371159b76d3f")), parentId: null),

            Account.Create(AccountId.Of(new Guid("cacfc56f-82ca-4cd5-b694-540b5e1b2e03")), "2001",
                name: "Cuentas por pagar",
                accountTypeId: AccountTypeId.Of(new Guid("d508e0ce-20d8-4e91-9e62-7a5d122aad2d")), parentId: null),

            Account.Create(AccountId.Of(new Guid("624817b2-cb16-434a-8ffa-896d6a88f6fa")), "3001", name: "Patrimonio",
                accountTypeId: AccountTypeId.Of(new Guid("77084a67-ab6c-4a75-b673-2094ad375c0b")), parentId: null),

            Account.Create(AccountId.Of(new Guid("23b79d9f-96d3-4c20-ac8e-4387a70152b3")), "4001", name: "Ventas",
                accountTypeId: AccountTypeId.Of(new Guid("6e705d5f-2d5c-449e-a187-17dc00d2c914")), parentId: null),

            Account.Create(AccountId.Of(new Guid("469c7a21-dfa2-4b73-a3b3-3c76842717f2")), "5001", name: "Sueldos",
                accountTypeId: AccountTypeId.Of(new Guid("71bbd6e0-abf4-4f2a-afec-9199bb404b08")), parentId: null),
        };

    public static IEnumerable<Period> Periods =>
        new List<Period>
        {
            Period.Create(PeriodId.Of(new Guid("e44ed594-272c-4978-a3b5-11fb47e9ca12")), year: DateTime.Now.Year,
                month: DateTime.Now.Month, start: new DateTime(2027, 7, 1), end: new DateTime(2027, 7, 31))
        };

    public static IEnumerable<JournalEntry> JournalEntriesWithLines
    {
        get
        {
            /*
             * journal Entry 1
             */
            var journalEntry1 = JournalEntry.Create(
                JournalEntryId.Of(new Guid("b45e75af-fe29-4c2e-9653-aad9bf5b42ef")),
                new DateTime(2027, 7, 10),
                "Pago de sueldos",
                PeriodId.Of(new Guid("e44ed594-272c-4978-a3b5-11fb47e9ca12"))
            );

            journalEntry1.AddLine(
                accountId: AccountId.Of(new Guid("469c7a21-dfa2-4b73-a3b3-3c76842717f2")),
                debit: 750000,
                credit: 0,
                lineNumber: 1
            );

            journalEntry1.AddLine(
                accountId: AccountId.Of(new Guid("7573e77c-110b-4a3b-9bda-7be306bb14b4")),
                debit: 0,
                credit: 750000,
                lineNumber: 2
            );


            /*
             * journal Entry 2
             */
            var journalEntry2 = JournalEntry.Create(
                JournalEntryId.Of(new Guid("0b55189d-ce04-471f-abbb-f73208be063a")),
                new DateTime(2027, 7, 12),
                "Ventas de producto",
                PeriodId.Of(new Guid("e44ed594-272c-4978-a3b5-11fb47e9ca12"))
            );

            journalEntry2.AddLine(
                accountId: AccountId.Of(new Guid("7573e77c-110b-4a3b-9bda-7be306bb14b4")),
                debit: 1256700,
                credit: 0,
                lineNumber: 1
            );

            journalEntry2.AddLine(
                accountId: AccountId.Of(new Guid("23b79d9f-96d3-4c20-ac8e-4387a70152b3")),
                debit: 0,
                credit: 1256700,
                lineNumber: 2
            );

            return new List<JournalEntry> { journalEntry1, journalEntry2 };
        }
    }
}