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

namespace Eventide4
{
    public static class ContentHandler
    {
#if DEBUG
        public static string contentDirectory = "C:/Projects/mono/Eventide4/assets/";
#endif
        public static SpriteFont font;
        public static Game game;
        public static GraphicsDeviceManager graphicsManager;
        public static SpriteBatch spriteBatch;
        public static Dictionary<string, Texture2D> textureList;

        public static void Initialize(Game parentGame, GraphicsDeviceManager graphics)
        {
            game = parentGame;
            game.Content.RootDirectory = "Content";
            graphicsManager = graphics;
            spriteBatch = new SpriteBatch(game.GraphicsDevice);
            textureList = new Dictionary<string, Texture2D>();
        }

        public static void LoadGlobalContent()
        {
            font = game.Content.Load<SpriteFont>("fonts/CPSB");
        }

        public static void ResetAssets()
        {
            foreach(Texture2D texture in textureList.Values)
            {
                texture.Dispose(); //should not be necessary if I can erase all references
            }
            textureList.Clear();
        }

        public static Texture2D RegisterTexture(string path)
        {
            Texture2D texture;
            if (!textureList.TryGetValue(path, out texture))
            {
#if DEBUG
                if (File.Exists(contentDirectory + path + ".jpg"))
                {
                    FileStream fileStream = new FileStream(contentDirectory + path + ".jpg", FileMode.Open);
                    texture = Texture2D.FromStream(game.GraphicsDevice, fileStream);
                    fileStream.Dispose();
                }
                else
                {
                    FileStream fileStream = new FileStream(contentDirectory + path + ".png", FileMode.Open);
                    texture = Texture2D.FromStream(game.GraphicsDevice, fileStream);
                    fileStream.Dispose();
                }
#else
                texture = Content.Load<Texture2D>(path);
#endif
                textureList.Add(path, texture);
            }
            return texture;
        }
    }
}
