using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eventide4.Systems
{
    public class Entity
    {
        // TODO: Add alternate method to add entity with known asset reference instead of looking up references from a path string.

        SpriteState spriteState;
        TextState textState;
        BodyState bodyState;
        Scene.Scene scene;

        public bool Visible { get; set; }

        public Entity(Scene.Scene scene)
        {
            this.scene = scene;
            Visible = true;
        }

        public Entity AddBody(float x = 0f, float y = 0f)
        {
            bodyState = new BodyState(x, y);
            scene.Physics.AddBody(bodyState);
            if (spriteState != null)
            {
                spriteState.AddBody(bodyState);
            }
            if (textState != null)
            {
                textState.AddBody(bodyState);
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

        public Entity AddText(string fontpath, string message, float x = 0f, float y = 0f, Color color = default(Color), float scale = 1f)
        {
            Library.Font font = GlobalServices.GlobalFonts.Register(fontpath);
            textState = new TextState(font, message, x, y, color, scale);
            if (bodyState != null)
            {
                textState.AddBody(bodyState);
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
            if (!Visible) return;
            if (spriteState != null) spriteState.Render();
            if (textState != null) textState.Render();
        }
    }
}
