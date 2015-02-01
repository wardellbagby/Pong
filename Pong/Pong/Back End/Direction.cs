using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Pong.Back_End
{
    public sealed class Direction
    {
        static double degree; //direction
        static float speed = 5;
        /*
         * This class wil be a bit interesting, 
         * it will (obvious) keep the direction of the ball
         * or( if called) randomize the direction on start
         * 
         * Randomizing will set it in a random direction between 20-50 degrees up or down, left or right.
         * This will make it harder for someone to cheat the game because the game will not be static         
         */
        public static void randomize()
        {
            Random rnd = new Random();
            degree = rnd.Next(0, 30) + 20;

            int UP=0, DOWN=-1, RIGHT =0, LEFT =-1;

            int upDown = rnd.Next(-1, 1);
            int rightLeft = rnd.Next(-1, 1);

            if(upDown == UP && rightLeft == LEFT)
            {
                degree = 180 - degree;
            }
            else if (upDown == DOWN && rightLeft == LEFT)
            {
                degree += 180;
            }
            else if(upDown == DOWN && rightLeft == RIGHT)
            {
                degree = 360 - degree;
            }
        }

        //i couldnt figure out how to pass by reference so i did this instead, crappy i know. sorry
        public static Vector2 getNextPoint(Vector2 origin)
        {
            //this needs smoothing
            float newX = origin.X +      (speed * (float)Math.Cos(DegreeToRadian(degree)));
            float newY = origin.Y + -1 * (speed * (float)Math.Sin(DegreeToRadian(degree)));
            return new Vector2(newX, newY);
        }

        private static double DegreeToRadian(double angle)
        {
            return Math.PI * angle / 180.0;
        }

        public static void changeX()
        {
            degree = (180 - degree) % 360;
            increaseSpeed();
        }

        public static void changeY()
        {
            degree = (360 - degree) % 360;
        }

        private static void increaseSpeed()
        {
            speed += 1;
        }

        public static void resetSpeed()
        {
            speed = 10;
        }
    }
}
