using System;
using System.Runtime.Serialization;

namespace Writely.Exceptions
{
    [Serializable]
    public class JournalNotFoundException : Exception
    {
        public JournalNotFoundException()
        {
        }

        protected JournalNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public JournalNotFoundException(string? message) : base(message)
        {
        }

        public JournalNotFoundException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}