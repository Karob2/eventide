using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eventide4
{
    public class Entity
    {
        public RenderComponent renderComponent;
        public UpdateComponent updateComponent;
        /*
        public bool canRender;
        public bool canUpdate;

        public Entity(bool cRender, bool cUpdate)
        {
            canRender = cRender;
            canUpdate = cUpdate;
        }
        */

        public static Entity Connect(RenderComponent renderComponent, UpdateComponent updateComponent)
        {
            Entity entity = new Entity(renderComponent, updateComponent);
            if(renderComponent != null) renderComponent.host = entity;
            if(updateComponent != null) updateComponent.host = entity;
            return entity;
        }

        public Entity(RenderComponent renderComp, UpdateComponent updateComp)
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
