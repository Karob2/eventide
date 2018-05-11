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
    public static class GlobalServices
    {
        public static string GameName { get; set; }
        public static string ContentDirectory { get; set; }
        public static string SaveDirectory { get; set; }

        public static Game Game { get; set; }
        public static GraphicsDeviceManager GraphicsManager { get; set; }
        public static ContentManager Content { get; set; }
        public static SpriteBatch SpriteBatch { get; set; }
        public static GameTime GameTime { get; set; }

        public static void Initialize(string gameName, Game game, GraphicsDeviceManager graphicsManager)
        {
            GameName = gameName;
            Game = game;
            GraphicsManager = graphicsManager;
            Content = game.Content;
            SpriteBatch = new SpriteBatch(game.GraphicsDevice);

#if DEBUG
            ContentDirectory = "../../../../../Content";
#else
            ContentDirectory = "Content";
#endif
            Content.RootDirectory = ContentDirectory;
            // TODO: How does this perform on linux? (And any other target OS.)
            SaveDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "My Games", gameName);
            // Ugh, should I wash input and output so that backslashes in custom content don't break linux?
            // Path.DirectorySeparatorChar
            Directory.CreateDirectory(SaveDirectory);
            /*
            string configPath = Path.Combine(SaveDirectory, "config.xml");
            if (!File.Exists(configPath))
                File.Create(configPath).Dispose();
            */
        }

        public static ContentManager NewContentManager()
        {
            return new ContentManager(Content.ServiceProvider, Content.RootDirectory);
        }
    }
}
