SELECT a."Code",
       a."Name",
       at."Name"                                 AS "AccountType",
       SUM(jl."DebitAmount" - jl."CreditAmount") AS "Balance"
from "JournalEntries" je
         JOIN "JournalEntryLines" jl ON je."Id" = jl."JournalEntryId"
         JOIN "Accounts" a ON jl."AccountId" = a."Id"
         JOIN "AccountTypes" at
ON a."AccountTypeId" = at."Id"
WHERE je."PeriodId" = @PeriodId
  AND je."CompanyId" = @CompanyId
  AND je."JournalEntryType" = 'Normal'
  AND at."Name" IN ('Activo'
    , 'Pasivo'
    , 'Patrimonio')
GROUP BY a."Code", a."Name", at."Name"
ORDER BY a."Code";