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
        private Vector2 position;

        public override void LoadAssets()
        {
            BackgroundColor = Color.Black;
            // TODO this paddle info is just for testing
            Paddle paddle = new Paddle(10, 40);
            Info.setPaddles(new Paddle[] { paddle, paddle });
        }

        public override void Update(GameTime gameTime)
        {
            Vector2 targetPosition = new Vector2(
                    ScreenManager.GraphicsDeviceManager.GraphicsDevice.Viewport.Width / 2 - (ScreenManager.Fonts["Default"].MeasureString("Insert Gameplay Here").X / 2),
                    ScreenManager.GraphicsDeviceManager.GraphicsDevice.Viewport.Height / 2 - (ScreenManager.Fonts["Default"].MeasureString("Insert Gameplay Here").Y / 2));

            position = Vector2.Lerp(position, targetPosition, 0.01f);
            if (Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                position.Y += 10;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                position.Y -= 10;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                position.X += 10;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                position.X -= 10;
            }
        }

        public override void Draw(GameTime gameTime)
        {
            ScreenManager.Sprites.Begin();
            ScreenManager.Sprites.DrawString(ScreenManager.Fonts["Default"], "Put in some game stuff here...", position, Color.White);

            // Get the paddles from the game info
            Paddle[] paddles = Info.getPaddles();
            GraphicsDevice graphicsDevice = ScreenManager.GraphicsDeviceManager.GraphicsDevice;

            int numberOfPaddles = 0;

            // Loop through the paddles and draw them
            foreach (Paddle paddle in paddles)
            {
                numberOfPaddles++;

                int paddleWidth = paddle.getWidth();
                if (paddleWidth <= 0)
                {
                    continue;
                }

                int paddleHeight = paddle.getHeight();
                if (paddleHeight <= 0)
                {
                    continue;
                }

                // TODO need to figure out how to tie a name to this Texture2D so that we can reference it in the Update function.
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
                    ScreenManager.Sprites.Draw(rectangleTexture, new Rectangle(0, Info.gameHeight / 2, paddleWidth, paddleHeight), Color.Crimson);
                }
                else
                {
                    ScreenManager.Sprites.Draw(rectangleTexture, new Rectangle(Info.gameWidth - paddleWidth, Info.gameHeight / 2, paddleWidth, paddleHeight), Color.Gray);
                }
            }

            ScreenManager.Sprites.End();
        }
        public override void UnloadAssets() { }
    }
}
