using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using System;
using System.IO;
using Eventide4.Util.Input;

namespace Eventide4
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphicsManager;
        const string gameName = "Eventide";

        public Game1()
        {
            graphicsManager = new GraphicsDeviceManager(this);
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            /*
            graphics.PreferredBackBufferWidth = GraphicsDevice.DisplayMode.Width;
            graphics.PreferredBackBufferHeight = GraphicsDevice.DisplayMode.Height;
            graphics.IsFullScreen = true;
            */
            graphicsManager.PreferredBackBufferWidth = 1280;
            graphicsManager.PreferredBackBufferHeight = 720;
            graphicsManager.ApplyChanges();

            GlobalServices.Initialize(gameName, this, graphicsManager);
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // TODO: Load global content here (like menu textures, so they aren't loaded and unloaded throughout the game).
            Scenes.Scene.Initialize();
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // I'm not sure if the Alt+F4 catch is necessary because I don't know if the inhertent Alt+F4 is present in
            //   all target platforms.
            if ((Keyboard.GetState().IsKeyDown(Keys.LeftAlt) || Keyboard.GetState().IsKeyDown(Keys.RightAlt))
                && Keyboard.GetState().IsKeyDown(Keys.F4))
                Exit();
#if DEBUG
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
                Keyboard.GetState().IsKeyDown(Keys.F1))
                Exit();
#endif
            //GlobalServices.GameTime = gameTime;
            GlobalServices.DeltaSeconds = (float)gameTime.ElapsedGameTime.TotalSeconds;
            GlobalServices.KeyHandler.Update();
            // TODO: Not all scenes and situations require the repeaters to be updated. Consider moving this into Scene classes.
            //   And when repeat updating is started again, run Reset() first to clear the repeat state.
            //GlobalServices.KeyConfig.UpdateRepeaters();
            Scenes.Scene.UpdateSceneControl();
            Scenes.Scene.UpdateScenePhysics();

            if (GlobalServices.KeyHandler.JustPressed(KeyType.Console))
            {
                // TODO/BUGFIX: Prevent opening a console screen on top of a console screen (via multiple ~ presses).
                Scenes.Scene.AddScene(new Scenes.ConsoleScene());
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            Scenes.Scene.RenderScene();

            base.Draw(gameTime);
        }
    }
}
