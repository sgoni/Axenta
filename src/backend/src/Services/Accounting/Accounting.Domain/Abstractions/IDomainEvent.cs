﻿using MediatR;

namespace Accounting.Domain.Abstractions;

public interface IDomainEvent : INotification
{
    Guid EventId => Guid.NewGuid();
    public DateTime OcurredOn => DateTime.Now;
    public string EventType => GetType().AssemblyQualifiedName;
}