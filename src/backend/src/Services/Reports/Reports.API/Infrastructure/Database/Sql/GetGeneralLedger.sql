SELECT je.Date,
       je.Description,
       jl.Debit,
       jl.Credit,
       (jl.Debit - jl.Credit) AS Movement
FROM JournalEntries je
         JOIN JournalEntryLines jl ON je.Id = jl.JournalEntryId
WHERE je.PeriodId = @PeriodId
  AND je.CompanyId = @CompanyId
  AND jl.AccountId = @AccountId
  AND je.IsReversed = false
ORDER BY je.Date;
