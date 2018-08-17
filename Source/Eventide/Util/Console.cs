using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eventide.Util
{
    public class Console
    {
        static LinkedList<string> log;
        static int length;

        LinkedListNode<string> index;
        int lastLength;

        public Console()
        {
            if (log == null)
                log = new LinkedList<string>();
        }

        public void ExecuteCommand(string command)
        {

        }

        public void LogMessage(string message)
        {
            // TODO: This is not thread-safe.
            log.AddLast(message);
            // TODO: This is a cheap way to limit the message history size.
            if (log.Count() > 1024)
                log.RemoveFirst();

            // Even if the log is pruned, keep track of length as an indicator of when the log has been updated.
            length++;
        }

        public bool HasUpdate()
        {
            if (length == lastLength)
                return false;
            lastLength = length;
            return true;
        }

        public string ReadFirst()
        {
            index = log.First;
            return index?.Value;
        }

        public string ReadLast()
        {
            index = log.Last;
            return index?.Value;
        }

        public string Read()
        {
            return index?.Value;
        }

        public string ReadNext()
        {
            index = index.Next;
            return index?.Value;
        }

        public string ReadPrevious()
        {
            index = index.Previous;
            return index?.Value;
        }
    }
}
