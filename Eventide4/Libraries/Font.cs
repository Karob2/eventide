using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Eventide4.Libraries
{
    //[Serializable]
    //[XmlRootAttribute("XnaContent", Namespace = "Microsoft.Xna.Framework", IsNullable = false)]
    public class Font
    {
        public string FontFile { get; set; }
        public float Scale { get; set; }

        SpriteFont spriteFont;
        [XmlIgnore]
        public SpriteFont SpriteFont { get { return spriteFont; } set { spriteFont = value; } }

        public Font()
        {

        }

        public void SetFont(SpriteFont spriteFont)
        {
            this.spriteFont = spriteFont;
            this.spriteFont.DefaultCharacter = '?';
        }

        public void Render(string message, float x, float y, Color color, float scale = 1f, float depth = 1f)
        {
            GlobalServices.SpriteBatch.DrawString(spriteFont, message, new Vector2(x, y),
                color, 0f, new Vector2(0f, 0f), Scale * scale, SpriteEffects.None, depth);
        }
    }
}
