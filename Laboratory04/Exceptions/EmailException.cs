using System;

namespace Laboratory04.Exceptions
{
    internal class EmailException : Exception
    {
        public EmailException(string message)
            : base(message)
        { }
    }
}
