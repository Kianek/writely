using System;
using System.Runtime.Serialization;

namespace Writely.Exceptions
{
    [Serializable]
    public class EmptyTagsException : Exception
    {
        public EmptyTagsException()
        {
        }

        protected EmptyTagsException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public EmptyTagsException(string? message) : base(message)
        {
        }

        public EmptyTagsException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}