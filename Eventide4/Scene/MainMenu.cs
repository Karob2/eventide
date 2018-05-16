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

        Systems.Entity stop1;
        StringBuilder message;

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

            stop1 = entity;
            message = new StringBuilder();

            // TODO: Consider moving this to the base Scene library, and Start() or Stop via a flag in the constructor.
            //   Though it might need to be started and stopped within certain scenes? Unless all typing popups are
            //   handled as scenes of their own.
            GlobalServices.TextHandler.Start();
        }

        public override void Update()
        {
            GlobalServices.TextHandler.DumpBuffer();
            base.Update();
            menu.Update();

            // TODO: Implement text input as a control that can be attached to an entity?
            //   Start() and Stop() TextHandler from within the entity?
            foreach (char c in GlobalServices.TextHandler.TextBuffer)
            {
                if (Char.IsLetterOrDigit(c) || Char.IsPunctuation(c) || Char.IsSymbol(c) || c == ' ')
                {
                    // TODO: Strip off characters out of the font range? (Convert to question mark or similar.)
                    message.Append(c);
                }
                else if (c == '\b')
                {
                    if (message.Length > 0)
                        message.Remove(message.Length - 1, 1);
                }
                /*
                else
                {
                    // To figure out which keys are being registered and what their codes are.
                    message.Append("(" + String.Format("0x{0:X}", Convert.ToByte(c)) + ")");
                }
                */
            }
            stop1.SetText(message.ToString());
        }
    }
}
