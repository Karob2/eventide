using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eventide4
{
    public interface IUpdateComponent
    {
        //void setHost(Entity _host);
        Entity host { get; set; }
        void Update();
    }
}
