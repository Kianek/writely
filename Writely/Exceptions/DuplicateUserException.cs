using System;

namespace Writely.Exceptions
{
    public class DuplicateUserException : Exception
    {
        public DuplicateUserException(string? message) : base(message)
        {
        }
    }
}