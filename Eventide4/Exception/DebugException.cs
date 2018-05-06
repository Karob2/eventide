using System;

namespace Eventide4
{
    //usage: throw new DebugException(string);

    public class DebugException : Exception
    {
        public DebugException()
        {
        }

        public DebugException(string message)
            : base(message)
        {
        }

        public DebugException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
