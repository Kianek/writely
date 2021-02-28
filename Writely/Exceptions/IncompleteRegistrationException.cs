using System;
using System.Runtime.Serialization;

namespace Writely.Exceptions
{
    public class IncompleteRegistrationException : Exception
    {
        public IncompleteRegistrationException()
        {
        }

        protected IncompleteRegistrationException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public IncompleteRegistrationException(string? message) : base(message)
        {
        }

        public IncompleteRegistrationException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}