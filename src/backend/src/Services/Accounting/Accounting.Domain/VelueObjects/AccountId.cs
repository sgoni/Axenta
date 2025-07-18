﻿namespace Accounting.Domain.VelueObjects;

public record AccountId
{
    private AccountId(Guid value)
    {
        Value = value;
    }

    public Guid Value { get; }

    public static AccountId Of(Guid value)
    {
        ArgumentNullException.ThrowIfNull(value);
        if (value == Guid.Empty) throw new DomainException("AccountId cannot be empty");

        return new AccountId(value);
    }
}