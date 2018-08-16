using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eventide4.Util;

namespace Eventide4.Libraries
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
            // TODO: if spriteconfig does not exist, try to load image as a default basic sprite?

            /*
            // The below code can write an XML object to see what the XML structure should be like.
            Sprite sprite2 = new Sprite();
            sprite2.SetTexture(textureLibrary.Register("ball"));
            sprite2.XCenter = 64f;
            sprite2.YCenter = 64f;
            XmlHelper<Sprite>.Save(Path.Combine(GlobalServices.SaveDirectory, "testsprite.xml") ,sprite2);
            */

            Pathfinder pathfinder = Pathfinder.Find(path, "sprites", Pathfinder.FileType.xml);
            if (pathfinder.Path == null)
            {
                // Load fallback asset.
                pathfinder = Pathfinder.Find("system:default", "sprites", Pathfinder.FileType.xml);
            }
            Sprite sprite = XmlHelper<Sprite>.Load(pathfinder.Path);
            Pathfinder.SetCurrentPath(pathfinder);
            sprite.SetTexture(textureLibrary.Register(sprite.TextureFile));
            Pathfinder.ClearCurrentPath(); // TODO: This develops loose ends too easily.
            return sprite;
        }
    }
}
