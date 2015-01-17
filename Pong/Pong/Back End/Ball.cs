using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pong.Back_End
{
    public class Ball
    {
        // Ball stats
        private static int size;
        private int speed;

        // Methods
        public int getSize()
        {
            return size;
        }

        public void setSize(int newSize)
        {
            size = newSize;
        }

        public int getSpeed()
        {
            return speed;
        }

        public void setSpeed(int newSpeed)
        {
            this.speed = newSpeed;
        }
    }
}
