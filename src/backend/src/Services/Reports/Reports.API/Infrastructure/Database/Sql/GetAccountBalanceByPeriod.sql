SELECT COALESCE(SUM(l."Debit"), 0) - COALESCE(SUM(l."Credit"), 0) AS balance
FROM "JournalEntryLines" l
         INNER JOIN "JournalEntries" j ON l."JournalEntryId" = j."Id"
WHERE l."AccountId" = @AccountId
  AND j."PeriodId" = @PeriodId;