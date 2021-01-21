using System;
using System.Runtime.Serialization;

namespace Writely.Exceptions
{
    [Serializable]
    public class EntryNotFoundException : Exception
    {
        public EntryNotFoundException()
        {
        }

        protected EntryNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public EntryNotFoundException(string? message) : base(message)
        {
        }

        public EntryNotFoundException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}