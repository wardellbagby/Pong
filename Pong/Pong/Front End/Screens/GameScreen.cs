using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
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
            ScreenManager.Sprites.End();
        }
        public override void UnloadAssets() { }
    }
}
