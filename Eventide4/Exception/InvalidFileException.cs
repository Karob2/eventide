using System;

namespace Eventide4.Exception
{
    public class InvalidFileException : System.Exception
    {
        public InvalidFileException()
        {
        }

        public InvalidFileException(string message)
            : base(message)
        {
        }

        public InvalidFileException(string message, System.Exception inner)
            : base(message, inner)
        {
        }
    }
}
