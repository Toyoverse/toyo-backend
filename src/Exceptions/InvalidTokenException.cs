using System;
using System.Runtime.Serialization;

namespace BackendToyo.Exceptions
{
    [Serializable]
    internal class InvalidTokenException : Exception
    {
        public InvalidTokenException()
        {
        }

        public InvalidTokenException(string message) : base(message)
        {
        }

        public InvalidTokenException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected InvalidTokenException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}