using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eventide4.Library
{
    public class TextureLibrary : Library<string, Texture2D>
    {
        protected override Texture2D Load(string path) {
            Texture2D texture;

            string fullPath = GlobalServices.Content.RootDirectory + "/" + path;
            if (File.Exists(fullPath + ".xnb"))
            {
                texture = GlobalServices.Content.Load<Texture2D>(fullPath + ".xnb");
            }
            else
            {
                if (File.Exists(fullPath + ".jpg"))
                    fullPath = fullPath + ".jpg";
                else
                    fullPath = fullPath + ".png";
                FileStream fileStream = new FileStream(fullPath, FileMode.Open);
                texture = Texture2D.FromStream(GlobalServices.Game.GraphicsDevice, fileStream);
                fileStream.Dispose();
            }
            return texture;
        }
    }
}
