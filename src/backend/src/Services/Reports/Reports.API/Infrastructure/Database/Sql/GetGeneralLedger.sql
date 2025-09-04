SELECT je."Date",
       je."Description",
       jl."DebitAmount",
       jl."CreditAmount",
       (jl."DebitAmount" - jl."CreditAmount") AS "Movement"
FROM "JournalEntries" je
         JOIN "JournalEntryLines" jl ON je."Id" = jl."JournalEntryId"
WHERE je."PeriodId" = @PeriodId
  AND je."CompanyId" = @CompanyId
  AND jl."AccountId" = @AccountId
  AND je."JournalEntryType" = 'Normal'
ORDER BY je."Date";
