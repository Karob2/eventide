using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eventide4.Systems
{
    public class Entity
    {
        SpriteState spriteState;
        BodyState bodyState;
        Scene.Scene scene;

        public Entity(Scene.Scene scene)
        {
            this.scene = scene;
        }

        public Entity AddBody(float x = 0f, float y = 0f)
        {
            bodyState = new BodyState(x, y, 10f, 10f);
            scene.Physics.AddBody(bodyState);
            if (spriteState != null)
            {
                spriteState.AddBody(bodyState);
            }
            return this;
        }

        public Entity AddSprite(string path, float x = 0f, float y = 0f)
        {
            Library.Sprite sprite = scene.SpriteLibrary.Register(path);
            spriteState = new SpriteState(sprite, x, y);
            if (bodyState != null)
            {
                spriteState.AddBody(bodyState);
            }
            return this;
        }

        public void Render()
        {
            spriteState.Render();
        }
    }
}
