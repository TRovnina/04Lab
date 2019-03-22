using System;

namespace Laboratory04.Exceptions
{
    internal class AgeException : Exception
    {
        public AgeException(string message)
            : base(message)
        { }
    }
}
