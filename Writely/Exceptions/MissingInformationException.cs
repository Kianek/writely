using System;
using System.Runtime.Serialization;

namespace Writely.Exceptions
{
    public class MissingInformationException : Exception
    {
        public MissingInformationException()
        {
        }

        protected MissingInformationException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public MissingInformationException(string? message) : base(message)
        {
        }

        public MissingInformationException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}