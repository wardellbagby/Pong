using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pong.Back_End
{
    public class Board
    {
        // Board stats
        private static int width;
        private static int height;

        // Methods
        public int getWidth()
        {
            return width;
        }

        public static void setWidth(int newWidth)
        {
            width = newWidth;
        }

        public int getHeight()
        {
            return height;
        }

        public static void setHeight(int newHeight)
        {
            height = newHeight;
        }
    }
}
