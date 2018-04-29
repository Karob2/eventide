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
    public class ImageComponent : RenderComponent
    {
        public Texture2D texture;
        public Vector2 position;

        public ImageComponent(string tex, Vector2 pos)
        {
            texture = ContentHandler.RegisterTexture(tex);
            position = pos;
        }

        public override Rectangle Boundary()
        {
            return new Rectangle();
        }

        public override void Render()
        {
            //ContentHandler.spriteBatch.Draw(texture, position, Color.White);
            Rectangle area = new Rectangle(0, 0, ContentHandler.graphicsManager.PreferredBackBufferWidth, ContentHandler.graphicsManager.PreferredBackBufferHeight);
            ContentHandler.spriteBatch.Draw(texture, area, null, Color.White);
            //ContentHandler.spriteBatch.Draw(texture, area, new Rectangle(0, 0, 1024, 512), Color.White);
        }
    }
}
