using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Serialization;
using System.Xml.Linq;

namespace Eventide4
{
    public class InputManager
    {
        public static void Initialize()
        {
            KeyConfig keyConfig = new KeyConfig();
            keyConfig.Default();
            XmlHelper<KeyConfig>.Save(Path.Combine(GlobalServices.SaveDirectory, "keyconfig.xml"), keyConfig);
        }
    }
}
