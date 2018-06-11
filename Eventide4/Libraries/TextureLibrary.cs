using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Eventide4.Util;

namespace Eventide4.Libraries
{
    public class TextureLibrary : Library<string, Texture2D>
    {
        ContentManager contentManager;

        public TextureLibrary()
        {
            contentManager = GlobalServices.NewContentManager();
        }

        protected override Texture2D Load(string path)
        {
            Texture2D texture;
            Pathfinder pathfinder = Pathfinder.Find(path, "textures", Pathfinder.FileType.image);
            if (pathfinder.Path == null)
            {
                // Load fallback asset.
                pathfinder = Pathfinder.Find("system:square", "textures", Pathfinder.FileType.image);
            }
            if (pathfinder.Ext.Equals("xnb"))
            {
                contentManager.RootDirectory = pathfinder.ContentPath;
                texture = contentManager.Load<Texture2D>(pathfinder.ContentFile);
                //texture = contentManager.Load<Texture2D>("ball");
            }
            else
            {
                FileStream fileStream = new FileStream(pathfinder.Path, FileMode.Open);
                texture = Texture2D.FromStream(GlobalServices.Game.GraphicsDevice, fileStream);
                fileStream.Dispose();
            }
            /*
            string fullPath = GlobalServices.ContentDirectory + "/" + path;
            if (File.Exists(fullPath + ".xnb"))
            {
                texture = contentManager.Load<Texture2D>(fullPath + ".xnb");
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
            */
            return texture;
        }

        protected override void Unload(Texture2D texture)
        {
            texture.Dispose();
        }

        public override void Unload()
        {
            contentManager.Unload();
            base.Unload();
        }
    }
}
