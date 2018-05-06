using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eventide4.Library
{
    public class Library
    {
        public static void Initialize()
        {
            GlobalServices.Content.RootDirectory = "Content";
            TextureLibrary.Initialize();
        }
    }
}
