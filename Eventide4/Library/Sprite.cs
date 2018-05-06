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
    public class Sprite
    {
        // TODO: TextureName may not be needed after the texture has been loaded.
        string TextureName;
        float XCenter;
        float YCenter;
        //[XmlElement("Color")]
        //public Color Color;

        Texture2D texture;

        public Sprite(string textureName, float xCenter, float yCenter)
        {
            TextureName = textureName;
            XCenter = xCenter;
            YCenter = yCenter;
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
