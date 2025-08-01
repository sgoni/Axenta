namespace Accounting.Application.Dtos;

public record DocumentReferenceDto(
    Guid Id,
    Guid JournalEntryId,
    string SourceType,
    Guid SourceId,
    string ReferenceNumber,
    string Description);