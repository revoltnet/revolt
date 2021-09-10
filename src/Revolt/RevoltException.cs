#nullable enable
using System;
using System.Runtime.Serialization;

namespace Revolt
{
    public class RevoltException : Exception
    {
        public RevoltException()
        {
        }

        protected RevoltException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public RevoltException(string? message) : base(message)
        {
        }

        public RevoltException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}