using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Eventide4.Systems
{
    public class TextState
    {
        string message;
        float x, y;
        public float X { get { return x; } set { x = value; } }
        public float Y { get { return y; } set { y = value; } }
        Color color;
        float scale;
        public Boolean Visible { get; set; }
        Libraries.Font font;
        public Libraries.Font Font { get { return font; } set { font = value; } }
        BodyState body;
        public Color Color { get { return color; } set { color = value; } }

        public string Message { get { return message; } set { message = value; } }
        //public float X { get { return x; } set { x = value; } }

        public TextState(Libraries.Font font, string message = "", float x = 0f, float y = 0f, Color color = default(Color), float scale = 1f)
        {
            this.font = font;
            this.message = message;
            this.x = x;
            this.y = y;
            this.color = color;
            this.scale = scale;
            Visible = true;
        }

        public void AddBody(BodyState body)
        {
            this.body = body;
        }

        public void Render()
        {
            if (Visible && font != null)
            {
                if (body != null)
                {
                    x = body.X;
                    y = body.Y;
                }
                font.Render(message, x, y, color, scale);
            }
        }
    }
}
