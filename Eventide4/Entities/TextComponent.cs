using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eventide4
{
    public class TextComponent : Component, IRenderComponent
    {
        public string text;
        public Vector2 position;
        public Color color;

        public TextComponent(String _text, Vector2 _position)
        {
            text = _text;
            position = _position;
            color = Color.Black;
        }

        public TextComponent(String _text, Vector2 _position, Color _color)
        {
            text = _text;
            position = _position;
            color = _color;
        }
        /*
        public Rectangle Boundary()
        {
            Vector2 size = ContentHandler.font.MeasureString(text);
            return new Rectangle((int)position.X, (int)position.Y, (int)size.X, (int)size.Y);
        }
        */
        public dynamic Request(RenderProperty property)
        {
            if (property == RenderProperty.Boundary)
            {
                Vector2 size = ContentHandler.font.MeasureString(text);
                return new Rectangle((int)position.X, (int)position.Y, (int)size.X, (int)size.Y);
            }
            return null;
        }
        public void Render()
        {
            //spriteBatch.DrawString(font, contentDirectory, new Vector2(100, 100), Color.Black);
            ContentHandler.spriteBatch.DrawString(ContentHandler.font, text, position,
                color, 0f, new Vector2(0f, 0f), 1f, SpriteEffects.None, 0.5f);
        }
    }
}
