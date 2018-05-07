using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Eventide4.Library
{
    public class SpriteLibrary : Library<string, Sprite>
    {
        TextureLibrary textureLibrary;

        public SpriteLibrary(TextureLibrary textureLibrary)
        {
            this.textureLibrary = textureLibrary;
        }

        protected override Sprite Load(string path)
        {
            Sprite sprite = new Sprite(path, 64f, 64f);
            sprite.SetTexture(textureLibrary.Register(path));
            return sprite;
        }
    }
}
