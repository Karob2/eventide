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
    // I don't like this being a class, but I haven't found a good way to make more performance-minded access to this
    // information without destructuring it.
    // If I'm going the class route, I might as well write the physics system as a single class with multiple instances.
    public class Item
    {
        public float X { get; set; }
        public float Y { get; set; }
        public float XVelocity { get; set; }
        public float YVelocity { get; set; }

        public Item(float x, float y, float xVelocity, float yVelocity)
        {
            this.X = x;
            this.Y = y;
            this.XVelocity = xVelocity;
            this.YVelocity = yVelocity;
        }
    }

    public class Physics
    {
        List<Item> itemList;
        List<Entity> owner;
        List<SpriteState> spriteState;

        public Physics()
        {
            itemList = new List<Item>();
            owner = new List<Entity>();
            spriteState = new List<SpriteState>();
        }

        public void AddItem(Entity owner, float x = 0f, float y = 0f, float xVelocity = 0f, float yVelocity = 0f, SpriteState spriteState = null)
        {
            this.itemList.Add(new Item(x, y, xVelocity, yVelocity));
            this.owner.Add(owner);
            this.spriteState.Add(spriteState);
        }

        public void RemoveItem(Entity owner)
        {
            int index = this.owner.IndexOf(owner);
            if (index != -1)
            {
                this.itemList.RemoveAt(index);
                this.owner.RemoveAt(index);
                this.spriteState.RemoveAt(index);
            }
        }

        public void Update()
        {
            for (int i = 0; i < itemList.Count; i++)
            {
                itemList[i].X += itemList[i].XVelocity * (float)GlobalServices.GameTime.ElapsedGameTime.TotalSeconds;
                itemList[i].Y += itemList[i].YVelocity * (float)GlobalServices.GameTime.ElapsedGameTime.TotalSeconds;
            }
        }

        public void Synchronize()
        {
            for (int i = 0; i < itemList.Count; i++)
            {
                if (spriteState[i] != null)
                {
                    spriteState[i].X = itemList[i].X;
                    spriteState[i].Y = itemList[i].Y;
                }
            }
        }
    }
}
