using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eventide4.Systems
{
    public class BodyState
    {
        /*
        private float x, y, xVelocity, yVelocity;
        private SpriteState spriteState;

        public float X { get { return x; } set { x = value; } }
        public float Y { get { return y; } set { y = value; } }
        public float XVelocity { get { return xVelocity; } set { xVelocity = value; } }
        public float YVelocity { get { return yVelocity; } set { yVelocity = value; } }
        public SpriteState SpriteState { get { return spriteState; } set { spriteState = value; } }
        */
        public float X { get; set; }
        public float Y { get; set; }
        public float XVelocity { get; set; }
        public float YVelocity { get; set; }
        //public Entity Owner { get; set; }
        public SpriteState SpriteState { get; set; }
        // Add tag list, for collision purposes
        // Should there be a "ControlState" object that runs from parameters read into a "ControlLibrary" (AI parameters)
        //   that handles collision response?

        public BodyState(SpriteState spriteState, float x, float y, float xVelocity, float yVelocity)
        {
            this.SpriteState = spriteState;
            this.X = x;
            this.Y = y;
            this.XVelocity = xVelocity;
            this.YVelocity = yVelocity;
        }
    }
}
