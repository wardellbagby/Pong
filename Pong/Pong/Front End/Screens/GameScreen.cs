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
            Info.setPaddles(new Paddle[] { paddle });
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

            // Loop through the paddles and draw them
            foreach (Paddle paddle in paddles)
            {
                int paddleWidth = paddle.getWidth();
                int paddleHeight = paddle.getHeight();
                Texture2D rectangleTexture = new Texture2D(graphicsDevice, paddleWidth, paddleHeight);
                Color[] data = new Color[paddleWidth * paddleHeight];
                for (int i = 0; i < data.Length; ++i)
                {
                    data[i] = Color.White;
                }
                rectangleTexture.SetData(data);

                ScreenManager.Sprites.Draw(rectangleTexture, new Rectangle(0, Info.gameHeight / 2, paddleWidth, paddleHeight), Color.Crimson);
            }

            ScreenManager.Sprites.End();
        }
        public override void UnloadAssets() { }
    }
}
