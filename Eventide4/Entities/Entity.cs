using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eventide4
{
    public class Entity
    {
        public IRenderComponent renderComponent;
        public IUpdateComponent updateComponent;
        /*
        public bool canRender;
        public bool canUpdate;

        public Entity(bool cRender, bool cUpdate)
        {
            canRender = cRender;
            canUpdate = cUpdate;
        }
        */

        public static Entity Connect(IRenderComponent renderComponent, IUpdateComponent updateComponent)
        {
            Entity entity = new Entity(renderComponent, updateComponent);
            if(renderComponent != null) renderComponent.setHost(entity);
            if(updateComponent != null) updateComponent.setHost(entity);
            return entity;
        }

        public Entity(IRenderComponent renderComp, IUpdateComponent updateComp)
        {
            renderComponent = renderComp;
            updateComponent = updateComp;
        }

        public void Render()
        {
            renderComponent.Render();
        }

        public void Update()
        {
            updateComponent.Update();
        }
    }
}
