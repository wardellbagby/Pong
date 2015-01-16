using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Manager = Pong.Front_End.ScreenManager.ScreenManager;

namespace Pong.Front_End.Screens
{
    class MainMenuScreen : GameScreen
    {

        public override void LoadAssets()
        {
            BackgroundColor = Color.White;
            base.LoadAssets();
        }

        public override void UnloadAssets()
        {

            base.UnloadAssets();
        }

        public override void Draw(Microsoft.Xna.Framework.GameTime gameTime)
        {
            Manager.Sprites.Begin();
            Manager.Sprites.DrawString(Manager.Fonts["Default Font"], "Start", new Microsoft.Xna.Framework.Vector2(250, 250), Microsoft.Xna.Framework.Color.Black);
            Manager.Sprites.End();
            base.Draw(gameTime);
        }

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            if (Mouse.GetState().LeftButton == ButtonState.Pressed)
            {
                Manager.ChangeScreens(this, new ErrorScreen());
            }
            base.Update(gameTime);
        }
    }
}
