using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pong.Back_End
{
    public class GameInfo
    {
        //Global Info
        public static int gameWidth = 1366;
        public static int gameHeight = 768;

        //Board info
        public static Paddle[] paddles;
        public static Ball ball;
        public static Board board;
        public static int[] scores;

        //Menu Info
        public static int playerAmount;
        public static double ballSpeed;

        public static String[] playerNames;

        //Methods

        public static Paddle[] getPaddles()
        {
            return paddles;
        }

        public static void setPaddles(Paddle[] newPaddles)
        {
            paddles = newPaddles;
        }

        public static Ball getBall()
        {
            return ball;
        }

        public static void setBall(Ball newBall)
        {
            ball = newBall;
        }

        public static Board getBoard()
        {
            return board;
        }

        public static void setBoard(Board newboard)
        {
            board = newboard;
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
            if (newPlayerAmount >= 0)
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
