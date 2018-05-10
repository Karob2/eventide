using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eventide4.Systems
{
    // TODO: If alternative visual states aren't needed, perhaps SpriteState could be incorporated directly into the Entity class.
    public class SpriteState
    {
        float x, y;
        //public float X { get { return x; } set { x = value; } }
        //public float Y { get { return y; } set { y = value; } }
        public Boolean Visible { get; set; }
        Library.Sprite sprite;
        BodyState body;

        public SpriteState(Library.Sprite sprite, float x = 0f, float y = 0f)
        {
            this.sprite = sprite;
            this.x = x;
            this.y = y;
            Visible = true;
        }

        public void AddBody(BodyState body)
        {
            this.body = body;
        }

        public void Render()
        {
            if (Visible && sprite != null)
            {
                if (body != null)
                {
                    x = body.X;
                    y = body.Y;
                }
                sprite.Render(x, y);
            }
        }
    }
}
