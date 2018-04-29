using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eventide4
{
    public interface IRenderComponent
    {
        void setHost(Entity _host);
        void Render();
    }
}
