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
using System.Xml;
using System.Xml.Serialization;
using System.Xml.Linq;

namespace Eventide4
{
    public static class XmlHelper<T> where T : class
    {
        public static T Load(string path)
        {
            XDocument document = XDocument.Load(path);
            string xml = document.ToString();
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            StringReader reader = new StringReader(xml);
            object obj = serializer.Deserialize(reader);
            T item = (T)obj;
            reader.Close();
            return item;
        }

        public static void Save(string path, T item)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            FileStream fs = new FileStream(path, FileMode.Create);
            TextWriter writer = new StreamWriter(fs, new UTF8Encoding());
            //serializer.Serialize(writer, item, new XmlSerializerNamespaces(new[] { new XmlQualifiedName("", "Microsoft.Xna.Framework") }));
            serializer.Serialize(writer, item, new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty }));
            writer.Close();
        }
    }
}
