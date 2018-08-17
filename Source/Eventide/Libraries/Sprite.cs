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

namespace Eventide.Libraries
{
    //[Serializable]
    //[XmlRootAttribute("XnaContent", Namespace = "Microsoft.Xna.Framework", IsNullable = false)]
    public class Sprite
    {
        // TODO: TextureName may not be needed after the texture has been loaded.
        public string TextureFile { get; set; }
        public float XCenter { get; set; }
        public float YCenter { get; set; }
        //[XmlElement("Color")]
        //public Color Color;

        Texture2D texture;

        public Sprite()
        {

        }

        public void SetTexture(Texture2D texture)
        {
            this.texture = texture;
        }

        public void Render(float x, float y)
        {
            GlobalServices.SpriteBatch.Draw(
                texture,
                new Vector2(x - XCenter, y - YCenter),
                null,
                Color.White,
                0f,
                Vector2.Zero,
                1f,
                SpriteEffects.None,
                0f
                );
        }
    }
}
