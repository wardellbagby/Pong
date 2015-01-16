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
            ScreenManager.AddFont("Menu Item");
        }
        public override void Update(GameTime gameTime)
        {
            Vector2 targetPosition = new Vector2(
                    ScreenManager.GraphicsDeviceMgr.GraphicsDevice.Viewport.Width / 2 - ScreenManager.Fonts["Menu Item"].MeasureString("Insert Gameplay Here").X / 2,
                    200);

            position = Vector2.Lerp(position, targetPosition, 0.05f);
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
            ScreenManager.Sprites.DrawString(ScreenManager.Fonts["Menu Item"], "Put in some game stuff here...", position, Color.White);
            ScreenManager.Sprites.End();
        }
        public override void UnloadAssets() { }
    }
}
