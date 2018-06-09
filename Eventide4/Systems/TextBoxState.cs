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
    public class TextBoxState
    {
        float x, y, width, height, padding;
        public float X { get { return x; } set { x = value; } }
        public float Y { get { return y; } set { y = value; } }
        Color color;
        float scale;
        public Boolean Visible { get; set; }
        Library.Font font;
        public Library.Font Font { get { return font; } set { font = value; } }
        BodyState body;
        public Color Color { get { return color; } set { color = value; } }

        TextContainer textContainer;
        //public TextContainer TextContainer { get { return textContainer; } set { textContainer = value; } }

        //public string Message { get { return message; } set { message = value; } }
        //public float X { get { return x; } set { x = value; } }

        public TextBoxState(Library.Font font, float x, float y, float width, float height, float padding, Color color = default(Color), string text = "")
        {
            this.font = font;
            this.x = x;
            this.y = y;
            this.width = width;
            this.height = height;
            this.padding = padding;
            this.color = color;
            Visible = true;

            textContainer = new TextContainer(font, width - padding * 2, height - padding * 2, text);
        }

        public void AddBody(BodyState body)
        {
            this.body = body;
        }

        public void Render()
        {
            if (Visible)
            {
                textContainer.RenderText(x + padding, y + padding); // TODO: Needs a font color option.
            }
        }

        public void Focus()
        {
            GlobalServices.TextHandler.Start();
        }

        public void Unfocus()
        {

        }

        public void Resize(float x, float y, float width, float height, float padding)
        {
            this.x = x;
            this.y = y;
            this.width = width;
            this.height = height;
            this.padding = padding;
            textContainer.SetSize(width - padding * 2, height - padding * 2);
        }
    }
}
