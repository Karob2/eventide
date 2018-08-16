using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Eventide4.Scenes
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

            Systems.Entity entity;

            entity = new Systems.Entity(this, entityList).AddBody(100f, 100f).AddSprite("ball");
            entity.SetVelocity(20f, 10f);

            entity = new Systems.Entity(this, entityList).AddText("default", "N/A", 5f, 5f, Color.Cyan, 1f);
            stop1 = entity;
            message = new StringBuilder("Type: ");

            //menu = new Systems.MenuGroup(Systems.MenuOrder.vertical, MenuSelect, MenuDeselect);
            menu = new Systems.MenuGroup();
            menu.Add(new Systems.Entity(this, entityList).AddText("default", "Start", 100f, 100f, menuTextColor, 1f));
            menu.Add(new Systems.Entity(this, entityList).AddText("default", "Config", 100f, 200f, menuTextColor, 1f));
            menu.Last().MenuControl.SetAction(Input.GameCommand.MenuConfirm, MCConfig);
            menu.Add(new Systems.Entity(this, entityList).AddText("default", "Exit", 100f, 300f, menuTextColor, 1f));
            menu.Last().MenuControl.SetAction(Input.GameCommand.MenuConfirm, MCExit);
            menu.SetSelect(MCSelect);
            menu.SetDeselect(MCDeselect);
            menu.Refresh();

            // TODO: Consider moving this to the base Scene library, and Start() or Stop via a flag in the constructor.
            //   Though it might need to be started and stopped within certain scenes? Unless all typing popups are
            //   handled as scenes of their own.
            //GlobalServices.TextHandler.Start();
        }

        public static void MCSelect(Systems.Entity entity)
        {
            entity.TextState.Color = Color.White;
        }
        public static void MCDeselect(Systems.Entity entity)
        {
            entity.TextState.Color = menuTextColor;
        }
        // TODO: Most commands probably don't need to know the calling entity, so consider making a no-parameter method list.
        public static void MCConfig(Systems.Entity entity)
        {
            Scene.AddScene(new MenuConfig());
        }
        public static void MCExit(Systems.Entity entity)
        {
            GlobalServices.Game.Exit();
        }

        public override void UpdateControl()
        {
            //GlobalServices.TextHandler.DumpBuffer();
            base.UpdateControl();
            //menu.Update();

            // TODO: Implement text input as a control that can be attached to an entity?
            //   Start() and Stop() TextHandler from within the entity?
            /*
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
                
                else
                {
                    // To figure out which keys are being registered and what their codes are.
                    message.Append("(" + String.Format("0x{0:X}", Convert.ToByte(c)) + ")");
                }
                
            }
            stop1.SetText(message.ToString());
            */

            if (GlobalServices.InputManager.JustPressed(Input.GameCommand.MenuCancel))
            {
                if (menu.CheckOrSelectLast()) GlobalServices.Game.Exit();
            }
        }
    }
}
