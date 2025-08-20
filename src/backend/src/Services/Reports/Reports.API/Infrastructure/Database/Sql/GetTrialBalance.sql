SELECT a.Code,
       a.Name,
       SUM(jl.Debit)             AS TotalDebit,
       SUM(jl.Credit)            AS TotalCredit,
       SUM(jl.Debit - jl.Credit) AS Balance
FROM JournalEntries je
         JOIN JournalEntryLines jl ON je.Id = jl.JournalEntryId
         JOIN Accounts a ON jl.AccountId = a.Id
WHERE je.PeriodId = @PeriodId
  AND je.CompanyId = @CompanyId
  AND je.IsReversed = false
GROUP BY a.Code, a.Name
ORDER BY a.Code;