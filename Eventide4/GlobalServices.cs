using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eventide4
{
    public static class GlobalServices
    {
        public static Game Game { get; set; }
        public static GraphicsDeviceManager GraphicsManager { get; set; }
        public static ContentManager Content { get; set; }
        public static SpriteBatch SpriteBatch { get; set; }
        public static GameTime GameTime { get; set; }

        public static void Initialize(Game game, GraphicsDeviceManager graphicsManager)
        {
            Game = game;
            GraphicsManager = graphicsManager;
            Content = game.Content;
            SpriteBatch = new SpriteBatch(game.GraphicsDevice);
        }
    }
}
