SELECT a."Code",
       a."Name",
       SUM(jl."DebitAmount")                     AS "TotalDebit",
       SUM(jl."CreditAmount")                    AS "TotalCredit",
       SUM(jl."DebitAmount" - jl."CreditAmount") AS "Balance"
FROM "JournalEntries" je
         JOIN "JournalEntryLines" jl
              ON je."Id" = jl."JournalEntryId"
         JOIN "Accounts" a
              ON jl."AccountId" = a."Id"
WHERE je."PeriodId" = @PeriodId
  AND je."CompanyId" = @CompanyId
  AND je."JournalEntryType" = 'Normal'
GROUP BY a."Code", a."Name"
ORDER BY a."Code";