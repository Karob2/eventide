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
            bodyState = new BodyState(x, y);
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

        public void SetVelocity(float xVelocity, float yVelocity)
        {
            bodyState.XVelocity = xVelocity;
            bodyState.YVelocity = yVelocity;
        }

        public void Render()
        {
            spriteState.Render();
        }
    }
}
