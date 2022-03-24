using System;

namespace BackendToyo.Exceptions
{
    public class InvalidPasswordException : Exception
    {
         public InvalidPasswordException(string message): base(message)
         {
         }
    }
}