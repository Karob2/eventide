using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eventide4.Systems
{
    // TODO: If alternative visual states aren't needed, perhaps SpriteState could be incorporated directly into the Entity class.
    public class SpriteState
    {
        public float X { get; set; }
        public float Y { get; set; }
        public Boolean Visible { get; set; }
        Library.Sprite sprite;

        public SpriteState(Library.Sprite sprite, float x = 0f, float y = 0f)
        {
            this.sprite = sprite;
            X = x;
            Y = y;
            Visible = true;
        }

        public void Render()
        {
            if (Visible && sprite != null)
            {
                sprite.Render(X, Y);
            }
        }
    }
}
