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
    public class KeyConfig
    {
        public KeyPair Up { get; set; }
        public KeyPair Down { get; set; }
        public KeyPair Left { get; set; }
        public KeyPair Right { get; set; }
        public KeyPair Select { get; set; }
        public KeyPair Back { get; set; }

        List<KeyPair> keyList;
        List<KeyPair> repeaterList;

        public KeyConfig()
        {
            // TODO: Don't allow menu default controls to be overridden.
            Up = new KeyPair(Keys.Up, Keys.W);
            Down = new KeyPair(Keys.Down, Keys.S);
            Left = new KeyPair(Keys.Left, Keys.A);
            Right = new KeyPair(Keys.Right, Keys.D);
            Select = new KeyPair(Keys.Enter, Keys.J);
            Back = new KeyPair(Keys.Escape, Keys.K);
        }

        public void FinalizeKeys()
        {
            // TODO: Come up with a much less clunky way to handle the list of keys.
            keyList = new List<KeyPair>();
            keyList.Add(Up);
            keyList.Add(Down);
            keyList.Add(Left);
            keyList.Add(Right);
            keyList.Add(Select);
            keyList.Add(Back);
            repeaterList = new List<KeyPair>();
            repeaterList.Add(Up);
            repeaterList.Add(Down);
            repeaterList.Add(Left);
            repeaterList.Add(Right);
        }

        /*
        KeyPair AddKeys(Keys key1, Keys key2, bool repeater = false)
        {
            KeyPair keys = new KeyPair(key1, key2);
            keyList.Add(keys);
            if (repeater) repeaterList.Add(keys);
            return keys;
        }
        */

        public void Update()
        {
            foreach (KeyPair key in keyList)
            {
                key.Update();
            }
        }

        public void UpdateRepeaters()
        {
            foreach (KeyPair key in repeaterList)
            {
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
    }
}
