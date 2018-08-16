using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eventide4.Util
{
    // TODO: If this class is not implemented more than once or twice, delete it and move the code to wherever it is used.
    public class MethodItem<T>
    {
        private Action<T> method;
        private T item;

        public MethodItem(Action<T> method, T item)
        {
            this.method = method;
            this.item = item;
        }

        public void Invoke()
        {
            method.Invoke(item);
        }
    }
}
