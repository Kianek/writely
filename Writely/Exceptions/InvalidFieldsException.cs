using System;

namespace Writely.Exceptions
{
    public class InvalidFieldsException : Exception
    {
        public InvalidFieldsException(string? message) : base(message)
        {
        }
    }
}