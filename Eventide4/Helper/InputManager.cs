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

            XmlSerializer serializer = new XmlSerializer(typeof(KeyConfig));
            FileStream fs = new FileStream(Path.Combine(GlobalServices.SaveDirectory,"keyconfig.xml"), FileMode.Create);
            TextWriter writer = new StreamWriter(fs, new UTF8Encoding());
            serializer.Serialize(writer, keyConfig);
            writer.Close();
        }
    }
}
