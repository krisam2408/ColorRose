using System;

namespace ColorRoseLib.Exceptions
{
    public class NotValidColorByteArrayException : Exception
    {
        public NotValidColorByteArrayException() : base(ExceptionMessage()) { }

        public NotValidColorByteArrayException(Exception innerException) : base(ExceptionMessage(), innerException) { }

        private static string ExceptionMessage()
        {
            return "The provided byte array must follow the standard ARGB or RGB standard";
        }
    }
}
