﻿namespace Axenta.BuildingBlocks.Exceptions;

public class ConflictException : ApplicationException
{
    public ConflictException(string message) : base(message)
    {
    }
}