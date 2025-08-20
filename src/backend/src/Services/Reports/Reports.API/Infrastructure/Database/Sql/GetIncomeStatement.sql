SELECT a."Code",
       a."Name",
       at."Name"                     AS "AccountType",
       SUM(jl."Credit" - jl."Debit") AS "Balance"
FROM "JournalEntries" je
         JOIN "JournalEntryLines" jl ON je."Id" = jl."JournalEntryId"
         JOIN "Accounts" a ON jl."AccountId" = a."Id"
         JOIN "AccountTypes" at
              ON a."AccountTypeId" = at."Id"
WHERE je."PeriodId" = @PeriodId
  AND je."CompanyId" = @CompanyId
  AND je."IsReversed" = false
  AND at."Name" IN ('Ingreso', 'Gasto')
GROUP BY a."Code", a."Name", at."Name";