using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eventide4
{
    public class Component
    {
        private Entity _host;
        public Entity host { get { return _host; } set { _host = value; } }
        /*
        public void setHost(Entity _host)
        {
            host = _host;
        }
        */
    }
}
