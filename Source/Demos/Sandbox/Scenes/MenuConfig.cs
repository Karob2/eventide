using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eventide;
using Eventide.Input;
using Eventide.Scenes;
using Eventide.Systems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Sandbox.Scenes
{
    public class MenuConfig : Scene
    {
        MenuGroup menu;

        public MenuConfig() : base(false)
        {
            // TODO: Is it okay that assets can only be loaded into the active scene's libraries?
            sceneType = SceneType.Menu;

            menu = new MenuGroup();
            menu.Add(new Entity(this, entityList).AddText("default", "854x480", 200f, 120f, MainMenu.menuTextColor, 1f));
            menu.Last().MenuControl.SetAction(GameCommand.MenuConfirm, MCRes480);
            menu.Add(new Entity(this, entityList).AddText("default", "1280x720", 200f, 220f, MainMenu.menuTextColor, 1f));
            menu.Last().MenuControl.SetAction(GameCommand.MenuConfirm, MCRes720);
            menu.Add(new Entity(this, entityList).AddText("default", "1920x1080", 200f, 320f, MainMenu.menuTextColor, 1f));
            menu.Last().MenuControl.SetAction(GameCommand.MenuConfirm, MCRes1080);
            menu.Add(new Entity(this, entityList).AddText("default", "Fullscreen", 200f, 420f, MainMenu.menuTextColor, 1f));
            menu.Last().MenuControl.SetAction(GameCommand.MenuConfirm, MCResFullscreen);
            menu.Add(new Entity(this, entityList).AddText("default", "Back", 180f, 520f, MainMenu.menuTextColor, 1f));
            menu.Last().MenuControl.SetAction(GameCommand.MenuConfirm, MCBack);
            menu.SetSelect(MainMenu.MCSelect);
            menu.SetDeselect(MainMenu.MCDeselect);
            menu.Refresh();
            // TODO/DEBUG: Allow typing in custom resolutions to see what happens. (Or could implement this in the upcoming console.)

            // TODO: This shouldn't be necessary:
            //GlobalServices.TextHandler.Stop();
        }

        public static void MCRes480(Entity entity)
        {
            GlobalServices.GraphicsManager.PreferredBackBufferWidth = 854;
            GlobalServices.GraphicsManager.PreferredBackBufferHeight = 480;
            GlobalServices.GraphicsManager.ApplyChanges();
        }

        public static void MCRes720(Entity entity)
        {
            GlobalServices.GraphicsManager.PreferredBackBufferWidth = 1280;
            GlobalServices.GraphicsManager.PreferredBackBufferHeight = 720;
            GlobalServices.GraphicsManager.ApplyChanges();
        }

        public static void MCRes1080(Entity entity)
        {
            GlobalServices.GraphicsManager.PreferredBackBufferWidth = 1920;
            GlobalServices.GraphicsManager.PreferredBackBufferHeight = 1080;
            GlobalServices.GraphicsManager.ApplyChanges();
        }

        public static void MCResFullscreen(Entity entity)
        {
            if (GlobalServices.GraphicsManager.IsFullScreen)
                GlobalServices.GraphicsManager.IsFullScreen = false;
            else
                GlobalServices.GraphicsManager.IsFullScreen = true;
            GlobalServices.GraphicsManager.ApplyChanges();
        }

        public void MCBack(Entity entity)
        {
            Scene.RemoveScene(this);
        }

        public override void Render()
        {
            Texture2D pixel = new Texture2D(GlobalServices.Game.GraphicsDevice, 1, 1);
            Color[] colorData = {
                        Color.White
                    };
            pixel.SetData<Color>(colorData);
            GlobalServices.SpriteBatch.Draw(pixel, new Rectangle(0, 0, GlobalServices.Game.GraphicsDevice.Viewport.Width, GlobalServices.Game.GraphicsDevice.Viewport.Height), new Color(0.5f, 0.5f, 0f, 0.5f));
            base.Render();
        }
    }
}
