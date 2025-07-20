namespace Accounting.Application.Dtos;

public record AuditLogDto(
    Guid Id,
    string Entity,
    string Action,
    Guid PerformedBy,
    DateTime PerformedAt,
    string Details
);