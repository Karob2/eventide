﻿using Microsoft.Xna.Framework;
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
        public static List<string> ExtensionDirectories { get; set; }

        public static Game Game { get; set; }
        public static GraphicsDeviceManager GraphicsManager { get; set; }
        public static ContentManager GlobalContent { get; set; }
        public static SpriteBatch SpriteBatch { get; set; }
        public static GameTime GameTime { get; set; }
        public static Library.TextureLibrary GlobalTextures { get; set; }
        public static Library.SpriteLibrary GlobalSprites { get; set; }

        public static void Initialize(string gameName, Game game, GraphicsDeviceManager graphicsManager)
        {
            GameName = gameName;
            Game = game;
            GraphicsManager = graphicsManager;
            GlobalContent = game.Content;
            SpriteBatch = new SpriteBatch(game.GraphicsDevice);

#if DEBUG
            ContentDirectory = Path.Combine("..","..","..","..","..","Content");
#else
            ContentDirectory = "Content";
#endif
            //GlobalContent.RootDirectory = Path.Combine(System.IO.Directory.GetCurrentDirectory(), ContentDirectory);
            //System.Diagnostics.Debug.WriteLine(Path.GetFullPath(ContentDirectory));
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
            string extensionPath = Path.Combine(SaveDirectory, "Extensions");
            Directory.CreateDirectory(extensionPath);
            ExtensionDirectories = new List<string>();
            // TODO: As a debug, all extensions are automatically loaded. I'm not certain what the final behaviour
            //   should be.
            foreach (string folder in Directory.GetDirectories(extensionPath))
            {
                //ExtensionDirectories.Add(Path.GetFileName(folder));
                ExtensionDirectories.Add(folder);
            }
            // TODO: Make sure extension folder names only include _0-9A-Za-z
/*
#if DEBUG
            System.Diagnostics.Debug.WriteLine("Pathfinder test:");
            System.Diagnostics.Debug.WriteLine(Pathfinder.Find("ball", "sprites", Pathfinder.FileType.image).Path);
            System.Diagnostics.Debug.WriteLine(Pathfinder.Find("ball", "spriteconfigs", Pathfinder.FileType.xml).Path);
            Pathfinder pf = Pathfinder.Find("kraken:/round/ball2", "sprites", Pathfinder.FileType.image);
            System.Diagnostics.Debug.WriteLine(pf.Path);
            System.Diagnostics.Debug.WriteLine(Pathfinder.Find("../rootball", "spriteconfigs", Pathfinder.FileType.xml, pf).Path);
            System.Diagnostics.Debug.WriteLine(Pathfinder.Find("superball", "spriteconfigs", Pathfinder.FileType.xml, pf).Path);
#endif
*/

            Library.TextureLibrary.Initialize();
            Library.SpriteLibrary.Initialize();

            GlobalTextures = new Library.TextureLibrary();
            Library.TextureLibrary.AddLibrary(GlobalTextures);

            GlobalSprites = new Library.SpriteLibrary(GlobalTextures);
            Library.SpriteLibrary.AddLibrary(GlobalSprites);
        }

        public static ContentManager NewContentManager()
        {
            return new ContentManager(GlobalContent.ServiceProvider, GlobalContent.RootDirectory);
        }
    }
}
