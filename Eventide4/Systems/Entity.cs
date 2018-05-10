using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eventide4.Systems
{
    public class Entity
    {
        public static Entity CreateEntity(Scene.Scene scene, string spritePath, float x, float y)
        {
            // caller needs to choose whether to have physics object
            // if spritepath is an image, load as a default basic sprite?
            Library.Sprite sprite = scene.SpriteLibrary.Register(spritePath);

            Entity entity = new Entity();
            BodyState bodyState = new BodyState(x, y, 10f, 10f);
            SpriteState spriteState = new SpriteState(sprite, x, y);
            spriteState.addBody(bodyState);
            entity.spriteState = spriteState;
            entity.bodyState = bodyState;
            scene.Physics.AddBody(bodyState);
            return entity;
        }

        public SpriteState spriteState;
        public BodyState bodyState;
        //public Physics physics;

        public Entity(SpriteState spriteState = null)
        {
            /*
            this.spriteState = spriteState;
            //this.physics = physics;
            if (spriteState != null)
            {

            }
            */
        }

        public void Render()
        {
            spriteState.Render();
        }
    }
}
