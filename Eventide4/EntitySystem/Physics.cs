using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eventide4.EntitySystem
{
    public class Physics
    {
        public List<Vector2> position;
        public List<Vector2> velocity;
        public List<Entity> owner;

        public void addItem(Vector2 position, Vector2 velocity, Entity owner)
        {
            this.position.Add(position);
            this.velocity.Add(velocity);
            this.owner.Add(owner);
        }

        public void removeItem(Entity owner)
        {
            int index = this.owner.IndexOf(owner);
            if (index != -1)
            {
                this.position.RemoveAt(index);
                this.velocity.RemoveAt(index);
                this.owner.RemoveAt(index);
            }
        }
    }
}
