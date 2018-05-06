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
        public struct Item
        {
            public float x, y;
            public float xVelocity, yVelocity;

            public Item(float x, float y, float xVelocity, float yVelocity)
            {
                this.x = x;
                this.y = y;
                this.xVelocity = xVelocity;
                this.yVelocity = yVelocity;
            }
        }
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
        }

        public void Synchronize()
        {
            for (int i = 0; i < itemList.Count; i++)
            {
                if (spriteState[i] != null)
                {
                    spriteState[i].X = itemList[i].x;
                    spriteState[i].Y = itemList[i].y;
                }
            }
        }
    }
}
