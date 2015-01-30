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
        private Vector2 ballPosition;
        private Vector2 ballSpeed;

        public override void LoadAssets()
        {
            BackgroundColor = Color.Black;
            maxX = ScreenManager.GraphicsDeviceManager.GraphicsDevice.Viewport.Width;
            maxY = ScreenManager.GraphicsDeviceManager.GraphicsDevice.Viewport.Height;

            // TODO this paddle info is just for testing
            paddleWidth = 10;
            paddleHeight = 40;
            Paddle paddle = new Paddle(paddleWidth, paddleHeight);
            Info.setPaddles(new Paddle[] { paddle, paddle });

            // Set the coordinates to draw the ball at the center of the screen
            ballPosition = new Vector2(maxX / 2, maxY / 2);
            // Set the ball's speed
            ballSpeed = new Vector2(50.0f, 50.0f);

            // Load the ball sprite
            ballTexture = ScreenManager.ContentManager.Load<Texture2D>("ball");
        }

        public override void Update(GameTime gameTime)
        {
            //Exit
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Environment.Exit(0);
            }

            // This vector is where we want the left paddle to start
            Vector2 playerStartPosition = new Vector2(0, Info.gameHeight / 2);

            // This position is the one we want the player to control
            playerPosition = Vector2.Lerp(playerPosition, playerStartPosition, 0.00f);
            if (Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                playerPosition.Y += 10;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                playerPosition.Y -= 10;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                playerPosition.X += 10;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                playerPosition.X -= 10;
            }

            // TODO need to create a target position for the opponent's paddle to randomly move
            Vector2 enemyStartPosition = new Vector2(Info.gameWidth - paddleWidth, Info.gameHeight / 2);
            enemyPosition = Vector2.Lerp(enemyPosition, enemyStartPosition, 0.01f);

            int paddleMaxX = maxX - paddleWidth;
            int paddleMaxY = maxY - paddleHeight;

            // Keep the player paddle within the bounds of the screen
            if (playerPosition.X > paddleMaxX)
            {
                playerPosition.X = paddleMaxX;
            }
            if (playerPosition.X < 0)
            {
                playerPosition.X = 0;
            }
            if (playerPosition.Y > paddleMaxY)
            {
                playerPosition.Y = paddleMaxY;
            }
            if (playerPosition.Y < 0)
            {
                playerPosition.Y = 0;
            }

            // // TODO maybe we should make the paddle change direction when reaching the bounds
            // Keep the enemy paddle within the bounds of the screen
            if (enemyPosition.X > paddleMaxX)
            {
                enemyPosition.X = paddleMaxX;
            }
            if (enemyPosition.X < 0)
            {
                enemyPosition.X = 0;
            }
            if (enemyPosition.Y > paddleMaxY)
            {
                enemyPosition.Y = paddleMaxY;
            }
            if (enemyPosition.Y < 0)
            {
                enemyPosition.Y = 0;
            }

            // If the ball passes either wall, reset its position
            if (ballPosition.X > maxX || ballPosition.X < 0)
            {
                ballPosition = new Vector2(maxX / 2, maxY / 2);
            }

            // Keep the ball within the Y-bounds
            if (ballPosition.Y > maxY)
            {
                // Make the ball bounce off the ceiling
            }
            if (ballPosition.Y < 0)
            {
                // Make the ball bounce off the floor
            }
        }

        public override void Draw(GameTime gameTime)
        {
            ScreenManager.Sprites.Begin();

            // Get the paddles from the game info
            Paddle[] paddles = Info.getPaddles();
            GraphicsDevice graphicsDevice = ScreenManager.GraphicsDeviceManager.GraphicsDevice;

            int numberOfPaddles = 0;

            // Loop through the paddles and draw them
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
                    ScreenManager.Sprites.Draw(rectangleTexture, new Rectangle(Info.gameWidth - paddleWidth, Info.gameHeight / 2, paddleWidth, paddleHeight), Color.Gray);
                }
            }

            // Draw the ball
            ScreenManager.Sprites.Draw(ballTexture, ballPosition, Color.White);

            ScreenManager.Sprites.End();
        }

        public override void UnloadAssets()
        {
            // Unload the content
            ScreenManager.ContentManager.Unload();
        }
    }
}
