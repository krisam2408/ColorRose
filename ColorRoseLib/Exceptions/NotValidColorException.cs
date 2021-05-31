using System;

namespace ColorRoseLib.Exceptions
{
    public class NotValidColorException : Exception
    {
        public string ColorParam { get; set; }

        public NotValidColorException(string colorParam) : base(ExceptionMessage(colorParam))
        {
            ColorParam = colorParam;
        }

        public NotValidColorException(string colorParam, Exception innerException) : base(ExceptionMessage(colorParam), innerException)
        {
            ColorParam = colorParam;
        }

        private static string ExceptionMessage(string colorParam)
        {
            bool isKnownName = true;
            if (colorParam[0] == '#')
                isKnownName = false;

            return isKnownName ? $"{colorParam} is not a known color name" : $"{colorParam} is not a valid color hexcode";
        }
    }
}
