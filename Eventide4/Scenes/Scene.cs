using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eventide4
{
    public class Scene
    {
        public enum SceneType
        {
            Menu,
            Level
        }
        public static Scene activeScene;

        public SceneType sceneType;
        public List<Entity> entityList;

        public Scene()
        {
            entityList = new List<Entity>();
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
            ContentHandler.spriteBatch.Begin();
            activeScene.Render();
            ContentHandler.spriteBatch.End();
        }

        public void Update()
        {
            foreach (Entity entity in entityList)
            {
                if (entity.updateComponent != null)
                {
                    entity.Update();
                }
            }
        }

        public void Render()
        {
            foreach(Entity entity in entityList)
            {
                if (entity.renderComponent != null)
                {
                    entity.Render();
                }
            }
        }
    }
}
