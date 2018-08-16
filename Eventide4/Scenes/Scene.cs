using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Eventide4.Scenes
{
    public class MethodQueueItem : Util.MethodItem<Systems.Entity>
    {
        public MethodQueueItem(Action<Systems.Entity> method, Systems.Entity entity) : base(method, entity) { }
    }

    // TODO: Consider moving the static components into a "SceneHandler" class.
    public class Scene
    {
        // TODO: Does this enum serve a purpose?
        public enum SceneType
        {
            Menu,
            Level
        }
        //static Scene activeScene;
        //public static Scene ActiveScene { get { return activeScene; } }
        static List<Scene> activeScene;
        //static Scene focusScene;

        protected SceneType sceneType;
        protected List<Systems.Entity> entityList;
        protected Systems.Physics physics;
        protected Libraries.TextureLibrary textureLibrary;
        protected Libraries.SpriteLibrary spriteLibrary;

        public Systems.Physics Physics { get { return physics; } }
        public Libraries.SpriteLibrary SpriteLibrary { get { return spriteLibrary; } }

        bool privateLibraries;

        List<MethodQueueItem> methodQueue;

        public Scene(bool privateLibraries)
        {
            this.privateLibraries = privateLibraries;

            entityList = new List<Systems.Entity>();

            physics = new Systems.Physics();

            if (privateLibraries)
            {
                textureLibrary = new Libraries.TextureLibrary();
                Libraries.TextureLibrary.AddLibrary(textureLibrary);
                spriteLibrary = new Libraries.SpriteLibrary(textureLibrary);
                Libraries.SpriteLibrary.AddLibrary(spriteLibrary);
            }
            else
            {
                textureLibrary = GlobalServices.GlobalTextures;
                spriteLibrary = GlobalServices.GlobalSprites;
            }

            methodQueue = new List<MethodQueueItem>();

            //GlobalServices.TextHandler.Stop();
        }

        public static void Initialize()
        {
            activeScene = new List<Scene> { new MainMenu() };
        }

        public static void ChangeScene(Scene scene)
        {
            activeScene.Clear();
            activeScene.Add(scene);
        }

        public static void AddScene(Scene scene)
        {
            activeScene.Add(scene);
        }

        public static void RemoveScene(Scene scene)
        {
            activeScene.Remove(scene);
        }

        public static void UpdateSceneControl()
        {
            activeScene.Last().UpdateControl();
        }

        public static void UpdateScenePhysics()
        {
            activeScene.Last().UpdatePhysics();
        }

        public static void RenderScene()
        {
            //ContentHandler.spriteBatch.Begin(samplerState: SamplerState.LinearWrap);
            GlobalServices.SpriteBatch.Begin();
            foreach (Scene scene in activeScene)
            {
                scene.Render();
            }
            GlobalServices.SpriteBatch.End();
        }

        public virtual void UpdateControl()
        {
            foreach (Systems.Entity entity in entityList)
            {
                entity.UpdateControl();
            }
            foreach (MethodQueueItem q in methodQueue)
            {
                q.Invoke();
            }
            methodQueue.Clear();
        }

        public void QueueMethod(Action<Systems.Entity> method, Systems.Entity item)
        {
            methodQueue.Add(new MethodQueueItem(method, item));
        }

        public virtual void UpdatePhysics()
        {
            physics.Update();
        }

        public virtual void Render()
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
                Libraries.TextureLibrary.RemoveLibrary(textureLibrary);
                Libraries.SpriteLibrary.RemoveLibrary(spriteLibrary);
            }
            // TODO: Should any other scene resources have forced disposal, or does dereferencing suffice?
        }
    }
}
