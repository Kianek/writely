using System;

namespace Writely.Exceptions
{
    public class PasswordMismatchException : Exception
    {
        public PasswordMismatchException(string? message) : base(message)
        {
        }
    }
}