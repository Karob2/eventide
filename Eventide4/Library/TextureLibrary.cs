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
#if DEBUG
            string fullPath = Program.contentDirectory + path;
            if (File.Exists(fullPath + ".jpg"))
                fullPath = fullPath + ".jpg";
            else
                fullPath = fullPath + ".png";
            FileStream fileStream = new FileStream(fullPath, FileMode.Open);
            Texture2D texture = Texture2D.FromStream(GlobalServices.Game.GraphicsDevice, fileStream);
            fileStream.Dispose();
#else
            Texture2D texture = GlobalServices.Content.Load<Texture2D>(path);
#endif
            return texture;
        }
    }
}
