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
    // TODO: Make a generic Library template so I don't have to duplicate TextureLibrary code here.
    public class SpriteLibrary : Library
    {
        Dictionary<string, Sprite> spriteList;

        public SpriteLibrary()
        {
            spriteList = new List<Sprite>();
        }

        public Sprite RegisterSprite()
        {
            //Sprite sprite = spriteList.Add(new Sprite("sprites/ball", 32, 32));
            Sprite sprite;

            // Search for loaded texture in local library and return if found.
            if (spriteList.TryGetValue(path, out sprite)) return sprite;

            // Search for loaded texture in all texture libraries and copy reference to local library if found.
            foreach (TextureLibrary tLibrary in libraries)
            {
                if (tLibrary == this) continue;
                if (tLibrary.textureList.TryGetValue(path, out texture))
                {
                    textureList.Add(path, texture);
                    return texture;
                }
            }

            // Otherwise, load the texture and store a reference in the local library.
#if DEBUG
            string fullPath = Program.contentDirectory + path;
            if (File.Exists(path + ".jpg"))
                fullPath = path + ".jpg";
            else
                fullPath = path + ".png";
            FileStream fileStream = new FileStream(fullPath, FileMode.Open);
            texture = Texture2D.FromStream(GlobalServices.Game.GraphicsDevice, fileStream);
            fileStream.Dispose();
#else
            texture = GlobalServices.Content.Load<Texture2D>(path);
#endif
            textureList.Add(path, texture);
            return texture;
        }

        public void RegisterSprite()
        {

        }
    }

    [Serializable]
    public class Sprite
    {
        string TextureName;
        int XCenter;
        int YCenter;
        //[XmlElement("Color")]
        //public Color Color;

        public Sprite()
        {
        }

        public Sprite(string textureName, int xCenter, int yCenter)
        {
            TextureName = textureName;
            XCenter = xCenter;
            YCenter = yCenter;
        }
    }
}
