using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eventide4.Scene
{
    public class Scene
    {
        public enum SceneType
        {
            Menu,
            Level
        }
        static Scene activeScene;

        protected SceneType sceneType;
        protected List<Systems.Entity> entityList;
        protected Systems.Physics physics;
        protected Library.TextureLibrary textureLibrary;
        protected Library.SpriteLibrary spriteLibrary;

        public Systems.Physics Physics { get { return physics; } }
        public Library.SpriteLibrary SpriteLibrary { get { return spriteLibrary; } }

        bool privateLibraries;

        public Scene(bool privateLibraries)
        {
            this.privateLibraries = privateLibraries;

            entityList = new List<Systems.Entity>();

            physics = new Systems.Physics();

            if (privateLibraries)
            {
                textureLibrary = new Library.TextureLibrary();
                Library.TextureLibrary.AddLibrary(textureLibrary);
                spriteLibrary = new Library.SpriteLibrary(textureLibrary);
                Library.SpriteLibrary.AddLibrary(spriteLibrary);
            }
            else
            {
                textureLibrary = GlobalServices.GlobalTextures;
                spriteLibrary = GlobalServices.GlobalSprites;
            }

            GlobalServices.TextHandler.Stop();
        }

        public static void Initialize()
        {
            activeScene = new MainMenu();
        }

        public static void UpdateScene()
        {
            activeScene.Update();
        }

        public static void RenderScene()
        {
            //ContentHandler.spriteBatch.Begin(samplerState: SamplerState.LinearWrap);
            GlobalServices.SpriteBatch.Begin();
            activeScene.Render();
            GlobalServices.SpriteBatch.End();
        }

        public virtual void Update()
        {
            physics.Update();
        }

        public void Render()
        {
            foreach (Systems.Entity entity in entityList)
            {
                entity.Render();
            }
        }

        public void Unload()
        {
            // Content disposal is automatically handled within the libraries:
            if (privateLibraries)
            {
                Library.TextureLibrary.RemoveLibrary(textureLibrary);
                Library.SpriteLibrary.RemoveLibrary(spriteLibrary);
            }
            // TODO: Should any other scene resources have forced disposal, or does dereferencing suffice?
        }
    }
}
