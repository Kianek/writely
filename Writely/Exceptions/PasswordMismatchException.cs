using System;
using System.Runtime.Serialization;

namespace Writely.Exceptions
{
    [Serializable]
    public class PasswordMismatchException : Exception
    {
        public PasswordMismatchException()
        {
        }

        protected PasswordMismatchException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public PasswordMismatchException(string? message) : base(message)
        {
        }

        public PasswordMismatchException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}