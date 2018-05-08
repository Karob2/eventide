using System;

namespace Eventide4.Exception
{
    //usage: throw new DebugException(string);

    public class DebugException : System.Exception
    {
        public DebugException()
        {
        }

        public DebugException(string message)
            : base(message)
        {
        }

        public DebugException(string message, System.Exception inner)
            : base(message, inner)
        {
        }
    }
}
