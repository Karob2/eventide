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

        public TextComponent(String str, Vector2 pos)
        {
            text = str;
            position = pos;
        }

        public void Render()
        {
            //spriteBatch.DrawString(font, contentDirectory, new Vector2(100, 100), Color.Black);
            ContentHandler.spriteBatch.DrawString(ContentHandler.font, text, position,
                Color.Black, 0f, new Vector2(0f, 0f), 1f, SpriteEffects.None, 0.5f);
        }
    }
}
