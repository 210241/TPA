using System;

namespace Reflection.MyException
{
    public class IoException : Exception
    {
        public IoException()
        {
        }

        public IoException(string message) : base(message)
        {
        }

        public IoException(string message, System.Exception innerException) : base(message, innerException)
        {
        }
    }
}