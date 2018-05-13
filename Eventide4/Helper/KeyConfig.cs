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

        [XmlAttribute]
        public string Key1 { get { return key1.ToString(); } set { key1 = (Keys)Enum.Parse(typeof(Keys), value); } }
        [XmlAttribute]
        public string Key2 { get { return key2.ToString(); } set { key2 = (Keys)Enum.Parse(typeof(Keys), value); } }

        public KeyPair()
        {

        }

        public KeyPair(Keys key1 = Keys.None, Keys key2 = Keys.None)
        {
            this.key1 = key1;
            this.key2 = key2;
        }
    }

    public class KeyConfig
    {
        public KeyPair Up { get; set; }
        public KeyPair Down { get; set; }
        public KeyPair Left { get; set; }
        public KeyPair Right { get; set; }
        public KeyPair Select { get; set; }
        public KeyPair Back { get; set; }

        public KeyConfig()
        {

        }

        public void Default()
        {
            Up = new KeyPair(Keys.Up, Keys.W);
            Down = new KeyPair(Keys.Down, Keys.S);
            Left = new KeyPair(Keys.Left, Keys.A);
            Right = new KeyPair(Keys.Right, Keys.D);
            Select = new KeyPair(Keys.Enter, Keys.J);
            Back = new KeyPair(Keys.Escape, Keys.K);
        }
    }
}
