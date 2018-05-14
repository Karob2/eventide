using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Eventide4
{
    public class KeyPair
    {
        Keys key1;
        Keys key2;
        bool pressed;
        bool toggled;
        bool ticked;
        int tickCount;
        float heldTime;

        [XmlAttribute]
        public string Key1 { get { return key1.ToString(); } set { key1 = (Keys)Enum.Parse(typeof(Keys), value); } }
        [XmlAttribute]
        public string Key2 { get { return key2.ToString(); } set { key2 = (Keys)Enum.Parse(typeof(Keys), value); } }

        public KeyPair()
        {
        }

        public KeyPair(Keys key1 = Keys.None, Keys key2 = Keys.None)// : this()
        {
            this.key1 = key1;
            this.key2 = key2;
            pressed = Keyboard.GetState().IsKeyDown(key1) || Keyboard.GetState().IsKeyDown(key2);
            toggled = false;
            ticked = false;
            tickCount = 0;
            heldTime = 0f;
        }

        public bool Held()
        {
            return pressed;
        }

        public bool JustPressed()
        {
            return pressed && toggled;
        }
        public bool JustReleased()
        {
            return !pressed && toggled;
        }

        public bool Ticked()
        {
            return ticked;
        }

        public void Update()
        {
            toggled = false;
            ticked = false;
            bool newState = Keyboard.GetState().IsKeyDown(key1) || Keyboard.GetState().IsKeyDown(key2);
            if (newState != pressed)
            {
                toggled = true;
            }
            pressed = newState;
        }

        public void UpdateRepeater()
        {
            // TODO: Replace hard-coded delay values with values loaded from config.xml
            ticked = false;
            if (pressed)
            {
                if (tickCount == 0)
                {
                    tickCount = 1;
                    heldTime = 0f;
                    ticked = true;
                }
                else
                {
                    float newHeldTime = heldTime + GlobalServices.DeltaSeconds;
                    if (tickCount == 1 && newHeldTime >= 0.5f)
                    {
                        ticked = true;
                        tickCount = 2;
                        heldTime = Math.Min(newHeldTime - heldTime, 0.2f); //prevent overflow ticking from fps lag
                    }
                    else if (tickCount > 1 && newHeldTime >= 0.08f)
                    {
                        ticked = true;
                        tickCount++;
                        heldTime = Math.Min(newHeldTime - heldTime, 0.08f);
                    }
                    else
                    {
                        heldTime = newHeldTime;
                    }
                }
            }
            else
            {
                tickCount = 0;
            }
        }

        public void Reset()
        {
            pressed = Keyboard.GetState().IsKeyDown(key1) || Keyboard.GetState().IsKeyDown(key2);
            toggled = false;
            ticked = false;
            tickCount = 0;
            heldTime = 0f;
        }
    }
}
