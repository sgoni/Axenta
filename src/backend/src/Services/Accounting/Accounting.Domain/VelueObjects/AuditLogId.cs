﻿namespace Accounting.Domain.VelueObjects;

// AuditLogId
public record AuditLogId : GuidValueObject
{
    public AuditLogId(Guid value) : base(value)
    {
    }

    public static AuditLogId Of(Guid value)
    {
        return new AuditLogId(value);
    }
}