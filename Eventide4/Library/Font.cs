using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eventide4.Library
{
    [Serializable]
    [System.Xml.Serialization.XmlRootAttribute("XnaContent", Namespace = "Microsoft.Xna.Framework", IsNullable = false)]
    public class Font
    {
        public string FontFile { get; set; }
        public float Scale { get; set; }

        SpriteFont spriteFont;

        public Font()
        {

        }

        public void SetFont(SpriteFont spriteFont)
        {
            this.spriteFont = spriteFont;
        }

        public void Render(string message, float x, float y, Color color, float scale, float depth = 1f)
        {
            GlobalServices.SpriteBatch.DrawString(spriteFont, message, new Vector2(x, y),
                color, 0f, new Vector2(0f, 0f), Scale * scale, SpriteEffects.None, depth);
        }
    }
}
