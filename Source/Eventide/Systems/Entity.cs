using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Eventide.Systems
{
    public class Entity
    {
        // TODO: Add alternate method to add entity with known asset reference instead of looking up references from a path string.

        MenuControl menuControl;
        SpriteState spriteState;
        TextState textState;
        BodyState bodyState;
        Scenes.Scene scene;

        public MenuControl MenuControl { get { return menuControl; } }
        public TextState TextState { get { return textState; } }
        public TextBoxState TextBoxState { get; set; }
        public BodyState BodyState { get { return bodyState; } }
        public Scenes.Scene Scene { get { return scene; } }

        public bool Visible { get; set; }
        public bool Active { get; set; }
        //public bool Selected { get; set; }

        public Entity(Scenes.Scene scene)
        {
            this.scene = scene;
            Visible = true;
            Active = true;
        }

        // Constructor to automatically add newly created entity to parent scene's entity list.
        public Entity(Scenes.Scene scene, List<Entity> entityList)
        {
            this.scene = scene;
            Visible = true;
            Active = true;
            entityList.Add(this);
        }

        public Entity AddMenuControl(MenuControl menuControl)
        {
            this.menuControl = menuControl;
            return this;
        }

        public Entity AddBody(float x = 0f, float y = 0f)
        {
            bodyState = new BodyState(x, y);
            scene.Physics.AddBody(bodyState);
            // TODO: Should I replace this clunky effeciency-focused linking with accessing each other through Entity/parent?
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
            Libraries.Sprite sprite = scene.SpriteLibrary.Register(path);
            spriteState = new SpriteState(sprite, x, y);
            if (bodyState != null)
            {
                spriteState.AddBody(bodyState);
            }
            return this;
        }

        public Entity AddText(string fontpath, string message, float x = 0f, float y = 0f, Color color = default(Color), float scale = 1f)
        {
            Libraries.Font font = GlobalServices.GlobalFonts.Register(fontpath);
            textState = new TextState(font, message, x, y, color, scale);
            if (bodyState != null)
            {
                textState.AddBody(bodyState);
            }
            return this;
        }

        public Entity AddTextBox(string fontpath, float x, float y, float width, float height, float padding, Color color = default(Color), string text = "")
        {
            Libraries.Font font = GlobalServices.GlobalFonts.Register(fontpath);
            TextBoxState = new TextBoxState(font, x, y, width, height, padding, color, text);
            return this;
        }

        public void SetText(string message)
        {
            if (textState != null)
            {
                textState.Message = message;
            }
        }

        /*
        // TODO: Each component having its own coordinates is getting sloppy. Perhaps Entity should keep the master
        //   coordinates, which are copied from the body, and to anything else that requests.
        public void SetTextPosition(float x, float y)
        {
            textState.X = x;
            textState.Y = y;
        }
        */

        public void SetVelocity(float xVelocity, float yVelocity)
        {
            bodyState.XVelocity = xVelocity;
            bodyState.YVelocity = yVelocity;
        }

        public void UpdateControl()
        {
            if (!Active) return;
            if (menuControl != null) menuControl.Update();
        }

        public void Render()
        {
            if (!Visible) return;
            if (spriteState != null) spriteState.Render();
            if (textState != null) textState.Render();
            if (TextBoxState != null) TextBoxState.Render();
        }
    }
}
