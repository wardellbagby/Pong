using Info = Pong.Back_End.GameInfo;
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

namespace Pong.Front_End.Screens
{
    class GameScreen : Screen
    {
        private int maxX;
        private int maxY;
        private int paddleWidth;
        private int paddleHeight;
        private Texture2D ballTexture;
        private Vector2 playerPosition;
        private Vector2 enemyPosition;
        private readonly int startSpeed = 10;
        private readonly int noSpeed = 0;
        private int oldMouseY=0;
        private Vector2 enemyStartPosition;
        int paddleMaxY;

        public override void LoadAssets()
        {
            BackgroundColor = Color.Black;
            maxX = ScreenManager.GraphicsDeviceManager.GraphicsDevice.Viewport.Width;
            maxY = ScreenManager.GraphicsDeviceManager.GraphicsDevice.Viewport.Height;

            // TODO this paddle info is just for testing
            paddleWidth = 20;
            paddleHeight = maxY/4;
            Paddle paddle = new Paddle(paddleWidth, paddleHeight);
            Info.setPaddles(new Paddle[] { paddle, paddle });

            // Set the coordinates to draw the ball at the center of the screen
            Ball.position = new Vector2(maxX / 2, maxY / 2);

            // Load the ball sprite
            ballTexture = ScreenManager.ContentManager.Load<Texture2D>("ball");


            // starting paddle locations
            Vector2 playerStartPosition = new Vector2(0, Info.gameHeight / 2);
            enemyStartPosition = new Vector2(Info.gameWidth - paddleWidth, Info.gameHeight / 2);

            // paddle positions
            playerPosition = Vector2.Lerp(playerPosition, playerStartPosition, 0.00f);
            enemyPosition = Vector2.Lerp(enemyStartPosition, enemyStartPosition, 0.00f);

            // get paddle bounds
            paddleMaxY = maxY - paddleHeight;
        }

        public override void Update(GameTime gameTime)
        {
            //TODO:  when the ball, and the paddles are moving, they begin to skip a little on screen, aka the transitions are not smooth at all, needs a fix

            //Exit
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Environment.Exit(0);
            }

            // keyboard detection
            if (Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                playerPosition.Y += 10;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                playerPosition.Y -= 10;
            }

            // mouse detection
            if (oldMouseY != Mouse.GetState().Y)
            {
                //mouse control
                playerPosition.Y = Mouse.GetState().Y;
                oldMouseY = Mouse.GetState().Y;
            }

            // if ball is not moving, and the space key is pressed, begin moving ball in random direction
            if(Ball.speed == noSpeed && Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                Direction.randomize();
                Ball.speed = startSpeed;
            }
            else if (Ball.speed != noSpeed)
            {
                Ball.updatePosition();

                //enemy "AI" - follows ball around screen
                int dist = 10;
                if (enemyPosition.Y > Ball.position.Y)
                {
                    enemyPosition.Y -= dist;
                }
                else if (enemyPosition.Y < Ball.position.Y)
                {
                    enemyPosition.Y += dist;
                }
            }

            // TODO need to create a target position for the opponent's paddle to randomly move

            // Keep the paddles within the bounds of the screen
            if (playerPosition.Y + paddleHeight > maxY)
            {
                playerPosition.Y = paddleMaxY;
            }
            if (playerPosition.Y < 0)
            {
                playerPosition.Y = 0;
            }
            if (enemyPosition.Y + paddleHeight > maxY)
            {
                enemyPosition.Y = paddleMaxY;
            }
            if (enemyPosition.Y < 0)
            {
                enemyPosition.Y = 0;
            }

            //collsion / X-Bounds detection
            if (Ball.position.X <= paddleWidth || Ball.position.X + ballTexture.Width >= maxX - paddleWidth)
            {
                if( (playerPosition.Y <= Ball.position.Y && playerPosition.Y + paddleHeight >= Ball.position.Y )  // player paddle
                    || 
                    (enemyPosition.Y <= Ball.position.Y && enemyPosition.Y + paddleHeight >= Ball.position.Y) // enemy paddle
                  ) // paddle stopped ball
                {
                    Direction.changeX();
                }
                else // if point was scored
                {
                    //TODO: count score
                    Ball.position = new Vector2(maxX / 2, maxY / 2);
                    Ball.speed = noSpeed;
                    Direction.resetSpeed();
                    enemyPosition = Vector2.Lerp(enemyPosition, enemyStartPosition, 0.01f);

                }
            }

            // Keep the ball within the Y-bounds
            if (Ball.position.Y >= maxY - ballTexture.Height || Ball.position.Y <= 0)
            {
                Direction.changeY();
            }
        }

        //checks paddle bounds


        public override void Draw(GameTime gameTime)
        {
            ScreenManager.Sprites.Begin();

            // Get the paddles from the game info
            Paddle[] paddles = Info.getPaddles();
            GraphicsDevice graphicsDevice = ScreenManager.GraphicsDeviceManager.GraphicsDevice;

            int numberOfPaddles = 0;

            // Loop through the paddles and draw them
            //NOTE Matt this foreach loop makes me cry.....
            foreach (Paddle paddle in paddles)
            {
                numberOfPaddles++;

                if (paddleWidth <= 0)
                {
                    continue;
                }

                int paddleHeight = paddle.getHeight();
                if (paddleHeight <= 0)
                {
                    continue;
                }

                Texture2D rectangleTexture = new Texture2D(graphicsDevice, paddleWidth, paddleHeight);
                
                // The following "data" stuff confuses me since the color doesn't actually show up, but if we don't do this then the rectangle won't show up.
                Color[] data = new Color[paddleWidth * paddleHeight];
                for (int i = 0; i < data.Length; ++i)
                {
                    data[i] = Color.White;
                }
                rectangleTexture.SetData(data);

                // Need to place the paddles on opposite sides of the game board
                if (numberOfPaddles % 2 == 1)
                {
                    ScreenManager.Sprites.Draw(rectangleTexture, playerPosition, new Rectangle(0, Info.gameHeight / 2, paddleWidth, paddleHeight), Color.Crimson);
                }
                else
                {
                    ScreenManager.Sprites.Draw(rectangleTexture, enemyPosition, new Rectangle(Info.gameWidth - paddleWidth, Info.gameHeight / 2, paddleWidth, paddleHeight), Color.Gray);
                }
            }

            // Draw the ball
            ScreenManager.Sprites.Draw(ballTexture, Ball.position, Color.White);

            ScreenManager.Sprites.End();
        }

        public override void UnloadAssets()
        {
            // Unload the content
            ScreenManager.ContentManager.Unload();
        }
    }
}
