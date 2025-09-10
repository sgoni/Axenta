SELECT COALESCE(SUM(l."DebitAmount"), 0) - COALESCE(SUM(l."CreditAmount"), 0) AS balance
FROM "JournalEntryLines" l
         INNER JOIN "JournalEntries" j ON l."JournalEntryId" = j."Id"
WHERE l."AccountId" = @AccountId
  AND j."PeriodId" = @PeriodId;