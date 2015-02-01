using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Pong.Back_End;
using Pong.Front_End.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pong.Back_End
{
    public sealed class Ball
    {
        // Ball stats
        public static int size;
        public static int speed;
        public static Vector2 position;

        public static void updatePosition()
        {
            position = Direction.getNextPoint(position);
        }
    }
}
