using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eventide4.Systems
{
    public class Physics
    {
        // Consider using a HashList to speed up deletion, though it would slow down addition.
        // Consider, instead of deleting entries, marking them as "unused" and then using a lookup to reuse unused slots before creating new ones.
        List<BodyState> bodyList;
        //List<Entity> owner;
        //List<SpriteState> spriteState;

        public Physics()
        {
            bodyList = new List<BodyState>();
        }

        public void AddBody(BodyState body)
        {
            this.bodyList.Add(body);
        }

        public void RemoveBody(BodyState body)
        {
            /*
            int index = this.owner.IndexOf(owner);
            if (index != -1)
            {
                this.bodyList.RemoveAt(index);
                this.owner.RemoveAt(index);
                this.spriteState.RemoveAt(index);
            }
            */
        }

        public void Update()
        {
            for (int i = 0; i < bodyList.Count; i++)
            {
                bodyList[i].X += bodyList[i].XVelocity * GlobalServices.DeltaSeconds;
                bodyList[i].Y += bodyList[i].YVelocity * GlobalServices.DeltaSeconds;
            }
        }
    }
}
