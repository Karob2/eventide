using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eventide4.Systems
{
    public class Entity
    {
        public SpriteState spriteState;
        //public Physics physics;

        public Entity(SpriteState spriteState = null)
        {
            this.spriteState = spriteState;
            //this.physics = physics;
            if (spriteState != null)
            {

            }
        }

        public void Render()
        {
            spriteState.Render();
        }
    }
}
