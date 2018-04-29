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
    public class MainMenu : Scene
    {
        //SpriteBatch spriteBatch;

        public MainMenu()
        {
            sceneType = SceneType.Menu;
            entityList.Add(new Entity(
                new ImageComponent("backdrops/cloudbg", new Vector2(0, 0)),
                null
                ));
            entityList.Add(new Entity(
                new TextComponent(
                    "UseKerning controls the layout\n" +
                    "of the font. If this value is\n" +
                    "true, kerning information will\n" +
                    "be used when placing characters.",
                    new Vector2(100, 100)),
                null
                ));
        }
        /*
        public new void Update()
        {

        }
        */
    }
}
