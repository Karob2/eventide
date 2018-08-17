using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Eventide.Input
{
    public class UniversalInputCombo
    {
        List<UniversalInput> inputList;
        bool invalidPress;

        //public List<UniversalInput> InputList { get { return inputList; } set { inputList = value; } }

        [XmlAttribute]
        public string Value { get { return ToString(); } set { FromString(value); } }

        public UniversalInputCombo()
        {
            inputList = new List<UniversalInput>();
        }

        public UniversalInputCombo(string str) : this()
        {
            FromString(str);
        }

        public void FromString(string str)
        {
            string[] strList = str.Split('+');
            for (int i = 0; i < strList.Length; i++)
            {
                //TODO: Catch errors?
                inputList.Add(new UniversalInput(strList[i]));
            }
        }

        public new string ToString()
        {
            StringBuilder sb = new StringBuilder();
            bool firstItem = true;
            foreach (UniversalInput item in inputList)
            {
                if (!firstItem)
                    sb.Append('+');
                else
                    firstItem = false;
                sb.Append(item.ToString());
            }

            return sb.ToString();
        }

        public bool GetPressedState()
        {
            if (inputList.Count == 0) return false;
            if (inputList.Count == 1)
            {
                return inputList.Last().GetPressedState();
            }
            foreach (UniversalInput item in inputList)
            {
                // Modifier keys.
                if (item != inputList.Last())
                {
                    if (!item.GetPressedState()) break;
                }
                // Main key.
                else
                {
                    if (item.GetPressedState())
                    {
                        // Only return true if all modifier keys were pressed before the main key was pressed.
                        return !invalidPress;
                    }
                    else
                    {
                        invalidPress = false;
                        return false;
                    }
                }
            }
            // If modifier keys were not held, then main key press shouldn't count.
            invalidPress = inputList.Last().GetPressedState();
            return false;
        }

        public void Reset()
        {
            invalidPress = false;
        }
    }

    public class UniversalInput
    {
        UniversalInputType universalInputType;
        Keys keyInput;
        GamepadInput gamepadInput;
        MouseInput mouseInput;
        //KeyModifiers keyModifiers;

        public UniversalInput() { }

        public UniversalInput(string str)
        {
            FromString(str);
        }

        public void FromString(string str)
        {
            string[] strItem = str.Split('.');
            if (strItem.Length != 2)
            {
                //TODO: throw exception
                return;
            }
            //TODO: catch for invalid strings in these enum conversions
            universalInputType = (UniversalInputType)Enum.Parse(typeof(UniversalInputType), strItem[0]);
            if (universalInputType == UniversalInputType.Keyboard)
            {
                keyInput = (Keys)Enum.Parse(typeof(Keys), strItem[1]);
            }
            else if (universalInputType == UniversalInputType.Mouse)
            {
                mouseInput = (MouseInput)Enum.Parse(typeof(MouseInput), strItem[1]);
            }
            else if (universalInputType == UniversalInputType.Gamepad)
            {
                gamepadInput = (GamepadInput)Enum.Parse(typeof(GamepadInput), strItem[1]);
            }
        }

        public new string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(universalInputType.ToString());
            sb.Append(".");
            if (universalInputType == UniversalInputType.Keyboard)
            {
                sb.Append(keyInput.ToString());
            }
            else if (universalInputType == UniversalInputType.Mouse)
            {
                sb.Append(mouseInput.ToString());
            }
            else if (universalInputType == UniversalInputType.Gamepad)
            {
                sb.Append(gamepadInput.ToString());
            }

            return sb.ToString();
        }

        public bool GetPressedState()
        {
            if (universalInputType == UniversalInputType.Keyboard)
            {
                return Keyboard.GetState().IsKeyDown(keyInput);
            }
            else if (universalInputType == UniversalInputType.Mouse)
            {
                return false;
            }
            else if (universalInputType == UniversalInputType.Gamepad)
            {
                return false;
            }
            return false;
        }
    }

    public class InputNode
    {
        GameCommand gameCommand;
        List<UniversalInputCombo> inputCombos;
        //bool ranged;
        bool pressed;
        bool toggled;
        bool ticked;
        int tickCount;
        float heldTime;

        [XmlIgnore]
        public GameCommand GameCommand { get { return gameCommand; } set { gameCommand = value; } }

        [XmlAttribute]
        public string Command
        {
            get
            {
                return gameCommand.ToString();
            }
            set
            {
                //Enum.TryParse<KeyType>(value, out type);
                gameCommand = (GameCommand)Enum.Parse(typeof(GameCommand), value);
                // TODO: Catch exception here when invalid keytype is in config file.
            }
        }

        public List<UniversalInputCombo> InputCombos { get { return inputCombos; } set { inputCombos = value; } }
        /*
        [XmlAttribute]
        public string Key1 { get { return key1.ToString(); } set { key1 = (Keys)Enum.Parse(typeof(Keys), value); } }
        [XmlAttribute]
        public string Key2 { get { return key2.ToString(); } set { key2 = (Keys)Enum.Parse(typeof(Keys), value); } }
        */

        public InputNode()
        {
            Clear();
        }

        public InputNode(GameCommand gameCommand) : this()
        {
            this.gameCommand = gameCommand;
        }

        public void Clear()
        {
            inputCombos = new List<UniversalInputCombo>();
        }

        //NOTE: What if the player has W and Shift+W for two different commands? Normally, Shift+W would execute Shift
        //command and W command, but in this case it needs to escape those and only execute Shift+W command.
        //Basically, if one scheme is a subset of another, then the larger one dominates.
        //I guess I could prebuild a list of dominators, for quick disabling of subordinates.
        //Even if I only allowed Shift, Ctrl, and Alt modifiers, I'd need to do something similar.
        //Though I could simplify to just checking the main (last) key.
        //Between Shift+C+F, Alt+F, and F, the longest would take priority.
        //What if the player releases Shift before F? Should it then count as if F is now held? Yes.

        //Cross-checking nodes sounds painful. Yes, let's pre-build. That needs to occur in InputManager.

        /*
        public void SetCombo(GameCommand gameCommand, String inputCombo)
        {
            this.gameCommand = gameCommand;
            inputCombos = new List<UniversalInputCombo> { new UniversalInputCombo(inputCombo) };
            Reset();
        }
        */

        public void SetCombo(String inputCombo)
        {
            inputCombos = new List<UniversalInputCombo> { new UniversalInputCombo(inputCombo) };
        }

        public void AddCombo(String inputCombo)
        {
            inputCombos.Add(new UniversalInputCombo(inputCombo));
            // TODO: Automatically sort input by type? (Keyboard, Touch, GamePad)
        }

        public void Reset()
        {
            foreach (UniversalInputCombo inputCombo in inputCombos)
            {
                inputCombo.Reset();
            }
            //pressed = Keyboard.GetState().IsKeyDown(key1) || Keyboard.GetState().IsKeyDown(key2);
            pressed = GetPressedState();
            toggled = false;
            ticked = false;
            tickCount = 0;
            heldTime = 0f;
        }

        public void Update()
        {
            toggled = false;
            ticked = false;
            //bool newState = Keyboard.GetState().IsKeyDown(key1) || Keyboard.GetState().IsKeyDown(key2);
            bool newState = GetPressedState();
            if (newState != pressed)
            {
                toggled = true;
            }
            pressed = newState;
        }

        bool GetPressedState()
        {
            foreach (UniversalInputCombo inputCombo in inputCombos)
            {
                if (inputCombo.GetPressedState()) return true;
            }
            return false;
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
    }
}
