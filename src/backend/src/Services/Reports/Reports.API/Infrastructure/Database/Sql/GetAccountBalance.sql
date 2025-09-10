SELECT COALESCE(SUM("DebitAmount"), 0) - COALESCE(SUM("CreditAmount"), 0) AS balance
FROM "JournalEntries" je
         INNER JOIN "JournalEntryLines" jel ON je."Id" = jel."JournalEntryId"
WHERE "AccountId" = @AccountId
  and "CompanyId" = @CompanyId;