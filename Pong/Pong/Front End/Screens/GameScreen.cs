using Info = Pong.Back_End.GameInfo;
using Microsoft.Xna.Framework;
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
        private Vector2 playerPosition;
        private Vector2 enemyPosition;
        private int paddleWidth;

        public override void LoadAssets()
        {
            BackgroundColor = Color.Black;
            // TODO this paddle info is just for testing
            paddleWidth = 10;
            Paddle paddle = new Paddle(paddleWidth, 40);
            Info.setPaddles(new Paddle[] { paddle, paddle });
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

                // TODO need to draw the ball. It'll be easiest to just save a ball picture and draw it as a sprite.
            }

            ScreenManager.Sprites.End();
        }
        public override void UnloadAssets() { }
    }
}
