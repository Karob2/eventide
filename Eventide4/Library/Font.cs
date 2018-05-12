using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eventide4.Library
{
    [Serializable]
    [System.Xml.Serialization.XmlRootAttribute("XnaContent", Namespace = "Microsoft.Xna.Framework", IsNullable = false)]
    public class Font
    {
        public float Scale { get; set; }

        public Font()
        {

        }

        public void Render(float x, float y)
        {

        }
    }
}
