using System;
using System.Runtime.Serialization;

namespace Writely.Exceptions
{
    [Serializable]
    public class DuplicateUserException : Exception
    {
        public DuplicateUserException()
        {
        }
        
        public DuplicateUserException(string? message) : base(message) { }

        protected DuplicateUserException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public DuplicateUserException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}