using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Eventide4
{
    public class MainMenu : Scene
    {
        //SpriteBatch spriteBatch;
        MenuGroup menu;

        public static readonly Color menuTextColor = new Color(0f, 0f, 0.5f);

        public MainMenu()
        {
            sceneType = SceneType.Menu;
            entityList.Add(Entity.Connect(
                new ImageComponent("backdrops/cloudbg", new Vector2(0, 0)),
                null
                ));
            entityList.Add(Entity.Connect(
                new TextComponent(
                    "UseKerning controls the layout\n" +
                    "of the font. If this value is\n" +
                    "true, kerning information will\n" +
                    "be used when placing characters.",
                    new Vector2(100, 100)),
                null
                ));

            menu = new MenuGroup();
            entityList.Add(Entity.Connect(
                new TextComponent("Start", new Vector2(100, 300), menuTextColor),
                new MenuComponent(menu)
                ));
            entityList.Add(Entity.Connect(
                new TextComponent("Config", new Vector2(100, 350), menuTextColor),
                new MenuComponent(menu)
                ));
            entityList.Add(Entity.Connect(
                new TextComponent("Exit", new Vector2(100, 400), menuTextColor),
                new MenuComponent(menu)
                ));

            /*
            menu.Start();
            entityList.Add(Entity.Connect(
                new TextComponent("Start", new Vector2(100, 200)),
                new MenuComponent()
                ));
            menu.End();
            */
        }
        /*
        public new void Update()
        {

        }
        */
    }
}
