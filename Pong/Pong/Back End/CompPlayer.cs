using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pong.Back_End
{
    public sealed class CompPlayer
    {
        Paddle paddle;
        Ball ball;

        public void setup(Paddle paddle, Ball ball)
        {
            this.paddle = paddle;
            this.ball = ball;
        }

        public void move()
        {
            
        }
    }
}
