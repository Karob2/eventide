using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eventide4
{
    public class Delegate<T>
    {
        public delegate void Method(T item);
    }

    // TODO: Does this oddly specific key container belong here?
    /*
    public class MethodItem<K, T>
    {
        public K item;// { get; set; }
        public Delegate<T>.Method method;// { get; set; }

        public MethodItem() { }

        public MethodItem(K item, Delegate<T>.Method method)
        {
            this.item = item;
            this.method = method;
        }
    }
    */

    public class MethodItem<T>
    {
        public T item;// { get; set; }
        public Delegate<T>.Method method;// { get; set; }

        //public MethodItem() { }

        public MethodItem(T item, Delegate<T>.Method method)
        {
            this.item = item;
            this.method = method;
        }

        public void Invoke()
        {
            method.Invoke(item);
        }
    }
}
