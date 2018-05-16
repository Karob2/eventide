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
        public List<KeyPair> KeyList { get; set; }

        public KeyConfig()
        {
            KeyList = new List<KeyPair>();
        }
    }
}
