using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eventide4
{
    public class SpriteComponent : Component, IRenderComponent
    {
        public Rectangle Boundary()
        {
            return new Rectangle();
        }

        public void Render()
        {

        }
    }
}
