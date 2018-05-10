using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using System;
using System.IO;

namespace Eventide4
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphicsManager;
        /*
        SpriteBatch spriteBatch;
        Texture2D ballTexture;
        Vector2 ballPosition;
        float ballSpeed;
        */
        //public SpriteFont font;

        public Game1()
        {
            graphicsManager = new GraphicsDeviceManager(this);
            //Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            /*
            ballPosition = new Vector2(graphics.PreferredBackBufferWidth / 2,
                graphics.PreferredBackBufferHeight / 2);
            ballSpeed = 100f;
            */
            /*
            graphics.PreferredBackBufferWidth = GraphicsDevice.DisplayMode.Width;
            graphics.PreferredBackBufferHeight = GraphicsDevice.DisplayMode.Height;
            graphics.IsFullScreen = true;
            */
            graphicsManager.PreferredBackBufferWidth = 1280;
            graphicsManager.PreferredBackBufferHeight = 720;
            graphicsManager.ApplyChanges();

            //GraphicsDevice.SamplerStates[0] = SamplerState.LinearWrap;

            //ContentHandler.Initialize(this, graphicsManager);
            GlobalServices.Initialize(this, graphicsManager);
#if DEBUG
            GlobalServices.Content.RootDirectory = "../../../../../Content";
#else
            GlobalServices.Content.RootDirectory = "Content";
            // TODO: Need to add an app storage directory for save files and custom content.
#endif
            Library.TextureLibrary.Initialize();
            Library.SpriteLibrary.Initialize();
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            //spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            //ballTexture = Content.Load<Texture2D>("ball");
            /*
            FileStream fileStream = new FileStream(contentDirectory + "sprites/ball.png", FileMode.Open);
            ballTexture = Texture2D.FromStream(GraphicsDevice, fileStream);
            fileStream.Dispose();
            */
            //font = Content.Load<SpriteFont>("fonts/CPSB");

            //ContentHandler.LoadGlobalContent();
            Scene.Scene.Initialize();
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
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            /*
            var kstate = Keyboard.GetState();

            if (kstate.IsKeyDown(Keys.Up))
                ballPosition.Y -= ballSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (kstate.IsKeyDown(Keys.Down))
                ballPosition.Y += ballSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (kstate.IsKeyDown(Keys.Left))
                ballPosition.X -= ballSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (kstate.IsKeyDown(Keys.Right))
                ballPosition.X += ballSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;

            ballPosition.X = Math.Min(Math.Max(ballTexture.Width / 2, ballPosition.X), graphics.PreferredBackBufferWidth - ballTexture.Width / 2);
            ballPosition.Y = Math.Min(Math.Max(ballTexture.Height / 2, ballPosition.Y), graphics.PreferredBackBufferHeight - ballTexture.Height / 2);
            */
            //ContentHandler.UpdateMouse();

            GlobalServices.GameTime = gameTime;

            Scene.Scene.UpdateScene();

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            //GraphicsDevice.Clear(Color.Transparent);
            GraphicsDevice.Clear(Color.Black);
            /*
#if DEBUG
                        GraphicsDevice.Clear(Color.CornflowerBlue);
#endif
            */
            // TODO: Add your drawing code here
            /*
            spriteBatch.Begin();
            //spriteBatch.Draw(ballTexture, ballPosition, Color.White);
            //spriteBatch.Draw(ballTexture, ballPosition, null, Color.White, 0f,
            //    new Vector2(ballTexture.Width / 2, ballTexture.Height / 2), Vector2.One, SpriteEffects.None, 0f);
            spriteBatch.Draw(ballTexture, new Rectangle((int)ballPosition.X - 50, (int)ballPosition.Y - 50, 100, 100), Color.White);
            //spriteBatch.DrawString(font, contentDirectory, new Vector2(100, 100), Color.Black);
            spriteBatch.DrawString(font, contentDirectory, new Vector2(100, 100), Color.Black, 0f, new Vector2(0f, 0f), 1f, SpriteEffects.None, 0.5f);
            spriteBatch.End();
            */
            Scene.Scene.RenderScene();

            base.Draw(gameTime);
        }
    }
}
