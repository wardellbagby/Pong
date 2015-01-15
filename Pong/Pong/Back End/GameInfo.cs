using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pong.Back_End
{
    public class GameInfo
    {
        //Board info
        public static Object[] paddles;
        public static Object ball;
        public static int[] scores;

        //Menu Info
        public static int playerAmount;
        public static double ballSpeed;

        public static String[] playerNames;

        //Methods

        public static Object[] getPaddles()
        {
            return paddles;
        }

        public static void setPaddles(Object[] newPaddles)
        {
            paddles = newPaddles;
        }

        public static Object getBall()
        {
            return ball;
        }

        public static void setBall(Object newBall)
        {
            ball = newBall;
        }

        public static int[] getScores()
        {
            return scores;
        }

        public static void setScores(int[] newScores)
        {
            scores = newScores;
        }

        public static int getPlayerAmount()
        {
            return playerAmount;
        }

        public static void setPlayerAmount(int newPlayerAmount)
        {
            if (playerAmount >= 0)
            {
                playerAmount = newPlayerAmount;
            }
        }

        public static double getBallSpeed()
        {
            return ballSpeed;
        }

        public static void setBallSpeed(double newBallSpeed)
        {
            ballSpeed = newBallSpeed;
        }

        public static String[] getPlayerNames()
        {
            return playerNames;
        }

        public static void setPlayerNames(String[] newPlayerNames)
        {
            playerNames = newPlayerNames;
        }
    }
}
