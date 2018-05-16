using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Eventide4
{
    public enum KeyType
    {
        Up,
        Down,
        Left,
        Right,
        Select,
        Back,
        MenuUp,
        MenuDown,
        MenuLeft,
        MenuRight,
        MenuSelect,
        MenuBack
    }

    public class KeyHandler
    {
        // TODO: Only update repeaters for menu keys.
        // TODO: Only run repeater updates when needed.

        KeyPair[] keyList;

        public KeyHandler()
        {
            keyList = new KeyPair[Enum.GetValues(typeof(KeyType)).Length];
            for (int i = 0; i < keyList.Length; i++)
            {
                keyList[i] = new KeyPair();
            }
            Default();
        }

        public KeyHandler(string path) : this()
        {
            LoadConfig(path);
        }

        public void Default()
        {
            Set(KeyType.Up, Keys.Up, Keys.W);
            Set(KeyType.Down, Keys.Down, Keys.S);
            Set(KeyType.Left, Keys.Left, Keys.A);
            Set(KeyType.Right, Keys.Right, Keys.D);
            Set(KeyType.Select, Keys.Enter, Keys.J);
            Set(KeyType.Back, Keys.Escape, Keys.K);
            // Menu/typing controls that shouldn't be configurable:
            Set(KeyType.MenuUp, Keys.Up);
            Set(KeyType.MenuDown, Keys.Down);
            Set(KeyType.MenuLeft, Keys.Left);
            Set(KeyType.MenuRight, Keys.Right);
            Set(KeyType.MenuSelect, Keys.Enter);
            Set(KeyType.MenuBack, Keys.Escape);
            Reset();
        }

        void Set(KeyType keyType, Keys key1 = Keys.None, Keys key2 = Keys.None)
        {
            keyList[(int)keyType].Set(keyType, key1, key2);
        }

        public void LoadConfig(string path)
        {
            if (!File.Exists(path))
            {
                Default();
                SaveConfig(path);
                return;
            }
            KeyConfig keyConfig = XmlHelper<KeyConfig>.Load(path);
            foreach (KeyPair keyPair in keyConfig.KeyList)
            {
                keyList[(int)keyPair.KeyType] = keyPair;
            }
            Reset();
        }

        public void SaveConfig(string path)
        {
            KeyConfig keyConfig = new KeyConfig();
            foreach (KeyType keyType in Enum.GetValues(typeof(KeyType)))
            {
                // Hacky way to prevent certain keys from being written to the XML file (so users don't change them).
                if ((int)keyType >= (int)KeyType.MenuUp) continue;

                keyConfig.KeyList.Add(keyList[(int)keyType]);
            }
            XmlHelper<KeyConfig>.Save(path, keyConfig);
        }

        public void Update()
        {
            foreach (KeyPair key in keyList)
            {
                key.Update();
                key.UpdateRepeater();
            }
        }

        public void Reset()
        {
            foreach (KeyPair key in keyList)
            {
                key.Reset();
            }
        }

        public bool Held(KeyType keyType)
        {
            return keyList[(int)keyType].Held();
        }

        public bool JustPressed(KeyType keyType)
        {
            return keyList[(int)keyType].JustPressed();
        }
        public bool JustReleased(KeyType keyType)
        {
            return keyList[(int)keyType].JustReleased();
        }

        public bool Ticked(KeyType keyType)
        {
            return keyList[(int)keyType].Ticked();
        }
    }
}
