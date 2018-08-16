using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Eventide4.Util
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
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.IndentChars = "    ";
            settings.Indent = true;
            settings.Encoding = Encoding.UTF8;
            XmlWriter writer = XmlWriter.Create(fs, settings);
            serializer.Serialize(writer, item, new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty }));
            writer.Close();
        }
    }
}
