using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eventide.Systems
{
    public class SpriteState
    {
        float x, y;
        //public float X { get { return x; } set { x = value; } }
        //public float Y { get { return y; } set { y = value; } }
        public Boolean Visible { get; set; }
        Libraries.Sprite sprite;
        BodyState body;

        public SpriteState(Libraries.Sprite sprite, float x = 0f, float y = 0f)
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
