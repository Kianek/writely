using System;
using System.Runtime.Serialization;

namespace Writely.Exceptions
{
    [Serializable]
    public class InvalidFieldsException : Exception
    {
        public InvalidFieldsException()
        {
        }

        protected InvalidFieldsException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public InvalidFieldsException(string? message) : base(message)
        {
        }

        public InvalidFieldsException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}