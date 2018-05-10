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

namespace Eventide4.Library
{
    public class SpriteLibrary : Library<string, Sprite>
    {
        TextureLibrary textureLibrary;

        public SpriteLibrary(TextureLibrary textureLibrary)
        {
            this.textureLibrary = textureLibrary;
        }

        protected override Sprite Load(string path)
        {
            // if spriteconfig does not exist, try to load image as a default basic sprite?
            XDocument document = XDocument.Load(GlobalServices.Content.RootDirectory + "/" + "spriteconfigs/" + path + ".xml");
            string xml = document.ToString();
            
            XmlSerializer serializer = new XmlSerializer(typeof(Sprite));
            StringReader reader = new StringReader(xml);
            object obj = serializer.Deserialize(reader);
            Sprite sprite = (Sprite)obj;
            reader.Close();
            sprite.SetTexture(textureLibrary.Register("sprites/" + sprite.TextureName));
            /*
            // The below code can write an XML object to see what the XML structure should be like.
            Sprite sprite = new Sprite();
            sprite.SetTexture(textureLibrary.Register("sprites/ball"));
            sprite.XCenter = 64f;
            sprite.YCenter = 64f;
            XmlSerializer serializer = new XmlSerializer(typeof(Sprite));
            FileStream fs = new FileStream(Program.contentDirectory + "spriteconfigs/test.xml", FileMode.Create);
            TextWriter writer = new StreamWriter(fs, new UTF8Encoding());
            serializer.Serialize(writer, sprite);
            writer.Close();
            */
            return sprite;
        }
    }
}
