using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Eventide4.Scene
{
    public class MainMenu : Scene
    {
        Systems.MenuGroup menu;
        public static readonly Color menuTextColor = new Color(0f, 0f, 0.5f);

        public MainMenu() : base(false)
        {
            // TODO: Is it okay that assets can only be loaded into the active scene's libraries?
            sceneType = SceneType.Menu;
            menu = new Systems.MenuGroup();

            Systems.Entity entity, entity2;

            entity = new Systems.Entity(this).AddBody(100f, 100f).AddSprite("ball");
            entity.SetVelocity(20f, 10f);
            entityList.Add(entity);
            entity2 = new Systems.Entity(this).AddBody(100f, 100f).AddSprite("ball_shift");
            entity2.SetVelocity(20f, 10f);
            entityList.Add(entity2);
            menu.Add(entity, entity2);

            entity = new Systems.Entity(this).AddBody(100f, 100f).AddText("default", "Run", 100f, 100f, menuTextColor, 1f);
            entity.SetVelocity(20f, 0f);
            entityList.Add(entity);
            entity2 = new Systems.Entity(this).AddBody(100f, 100f).AddText("default", "Run", 100f, 100f, Color.White, 1f);
            entity2.SetVelocity(20f, 0f);
            entityList.Add(entity2);
            menu.Add(entity, entity2);

            entity = new Systems.Entity(this).AddText("default", "Stop", 100f, 300f, Color.Red, 2f);
            entityList.Add(entity);
            entity2 = new Systems.Entity(this).AddText("default", "Stop", 100f, 300f, Color.White, 2f);
            entityList.Add(entity2);
            menu.Add(entity, entity2);
        }
        
        public override void Update()
        {
            base.Update();
            menu.Update();
        }
    }
}
