using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Eventide4.Input
{
    public class InputConfig
    {
        public List<InputNode> InputNodes { get; set; }

        public InputConfig()
        {
            InputNodes = new List<InputNode>();
        }
    }

    public class InputManager
    {
        // TODO: Only update repeaters for menu keys.
        // TODO: Only run repeater updates when needed.
        // TODO: Only update game/menu key status when needed.

        InputNode[] inputNodeList;

        public InputManager()
        {
            // TODO: GameCommand list should be set by the application, not the engine, so enum may not be good.
            inputNodeList = new InputNode[Enum.GetValues(typeof(GameCommand)).Length];
            for (int i = 0; i < inputNodeList.Length; i++)
            {
                inputNodeList[i] = new InputNode((GameCommand)i);
            }
            Default();
        }

        public InputManager(string path) : this()
        {
            LoadConfig(path);
        }

        public void Default()
        {
            Set(GameCommand.Up, Keys.Up, Keys.W);
            Set(GameCommand.Down, Keys.Down, Keys.S);
            Set(GameCommand.Left, Keys.Left, Keys.A);
            Set(GameCommand.Right, Keys.Right, Keys.D);
            Set(GameCommand.Action1, Keys.Enter, Keys.J);
            Set(GameCommand.Action2, Keys.Escape, Keys.K);
            // Menu controls that shouldn't be configurable in-game (at least the first values):
            Set(GameCommand.MenuUp, Keys.Up, Keys.W);
            Set(GameCommand.MenuDown, Keys.Down, Keys.S);
            Set(GameCommand.MenuLeft, Keys.Left, Keys.A);
            Set(GameCommand.MenuRight, Keys.Right, Keys.D);
            Set(GameCommand.MenuConfirm, Keys.Enter, Keys.J);
            Set(GameCommand.MenuCancel, Keys.Escape, Keys.K);
            Set(GameCommand.Console, Keys.OemTilde);
            // Controls that perhaps shouldn't be configurable in the cfg file either.
            Set(GameCommand.ConsoleConfirm, Keys.Enter);
            Set(GameCommand.ConsoleCancel, Keys.Escape);
            Reset();
        }

        void Set(GameCommand command, params System.Enum[] keys)
        {
            inputNodeList[(int)command].Clear();
            for (int i = 0; i < keys.Length; i++)
            {
                inputNodeList[(int)command].AddCombo("Keyboard." + keys[i].ToString());
            }
        }

        public void LoadConfig(string path)
        {
            if (!File.Exists(path))
            {
                Default();
                SaveConfig(path);
                return;
            }
            InputConfig inputConfig = Util.XmlHelper<InputConfig>.Load(path);
            foreach (InputNode inputNode in inputConfig.InputNodes)
            {
                inputNodeList[(int)inputNode.GameCommand] = inputNode;
            }
            Reset();
        }

        public void SaveConfig(string path)
        {
            InputConfig inputConfig = new InputConfig();
            foreach (GameCommand command in Enum.GetValues(typeof(GameCommand)))
            {
                // Hacky way to prevent certain keys from being written to the XML file (so users don't change them).
                if ((int)command >= (int)GameCommand.MenuUp) continue;

                inputConfig.InputNodes.Add(inputNodeList[(int)command]);
            }
            Util.XmlHelper<InputConfig>.Save(path, inputConfig);
        }

        public void Update()
        {
            foreach (InputNode node in inputNodeList)
            {
                node.Update();
                node.UpdateRepeater();
            }
        }

        public void Reset()
        {
            foreach (InputNode node in inputNodeList)
            {
                node.Reset();
            }
        }

        // Valid methods:
        // - KeyHandler.Held(KeyType.Up)
        // - KeyHandler.KeyList[KeyType.Up].Held()
        // Old, defunct method:
        // - KeyHandler.Up.Held()
        public bool Held(GameCommand command)
        {
            return inputNodeList[(int)command].Held();
        }

        public bool JustPressed(GameCommand command)
        {
            return inputNodeList[(int)command].JustPressed();
        }
        public bool JustReleased(GameCommand command)
        {
            return inputNodeList[(int)command].JustReleased();
        }

        public bool Ticked(GameCommand command)
        {
            return inputNodeList[(int)command].Ticked();
        }
    }
}
