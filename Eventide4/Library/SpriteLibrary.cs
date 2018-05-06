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
        protected override Sprite Load(string path)
        {
            return new Sprite("ball", 32, 32);
        }
    }
}
