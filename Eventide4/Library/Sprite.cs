using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eventide4.Library
{
    [Serializable]
    public class Sprite
    {
        string TextureName;
        int XCenter;
        int YCenter;
        //[XmlElement("Color")]
        //public Color Color;

        public Sprite(string textureName, int xCenter, int yCenter)
        {
            TextureName = textureName;
            XCenter = xCenter;
            YCenter = yCenter;
        }
    }
}
