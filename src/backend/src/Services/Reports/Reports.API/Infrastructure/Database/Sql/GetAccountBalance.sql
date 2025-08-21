SELECT COALESCE(SUM("Debit"), 0) - COALESCE(SUM("Credit"), 0) AS balance
FROM "JournalEntryLines"
WHERE "AccountId" = @AccountId;