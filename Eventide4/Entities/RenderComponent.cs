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
    /*
    public enum HighlightMode
    {
        None,
        Light,
        Dark,
        Gray,
        Border
    }
    */
    public class RenderComponent : Component
    {
        public virtual Rectangle Boundary()
        {
            return new Rectangle();
        }
        //void Highlight(HighlightMode mode);
        public virtual void Render()
        {

        }
    }
}
