﻿namespace Axenta.BuildingBlocks.Exceptions;

public class InternalServerException : ApplicationException
{
    public InternalServerException(string message) : base(message)
    {
    }

    public InternalServerException(string message, string details) : base(message)
    {
        Details = details;
    }

    public string? Details { get; }
}