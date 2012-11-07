using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace WindowsGame2
{
    class StatusFrames
    {
        public Rectangle[] frames;

        private int numFrames {get; set;}
        private int position {get; set;}

        public StatusFrames(int numFrames)
        {
            this.numFrames = numFrames;
            frames = new Rectangle[numFrames];
            position = -1;
        }

        public Rectangle getFrame()
        {
            position++;
            if (position % numFrames == 0)
                position = 0;

            return frames[position];
        }

    }
}
