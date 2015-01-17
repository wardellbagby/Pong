using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Info = Pong.Back_End.GameInfo;
using Microsoft.Xna.Framework.Graphics;
using Pong.Front_End.Managers;

namespace Pong.Front_End.Screens {
    class MainMenuScreen : Screen {
        private SpriteBatch spriteBatch;
        private List<MenuItem> menuItems = new List<MenuItem>();
        private int selectedIndex = 0;

        private int SelectedIndex {
            get { return selectedIndex; }
            set { selectedIndex = value >= menuItems.Count ? 0 : value < 0 ? menuItems.Count - 1 : value; }
        }

        public override void LoadAssets() {
            spriteBatch = ScreenManager.Sprites;
            BackgroundColor = Color.Black;
            ScreenManager.AddFont("Title");
            ScreenManager.AddFont("Menu Item");

            Vector2 startTextSize = ScreenManager.Fonts["Menu Item"].MeasureString("Start");
            Vector2 optionsTextSize = ScreenManager.Fonts["Menu Item"].MeasureString("Options");
            Vector2 aboutTextSize = ScreenManager.Fonts["Menu Item"].MeasureString("About");
            Vector2 startTextPosition = new Vector2(Info.gameWidth / 2, (int)(Info.gameHeight * .6f));
            Vector2 optionsTextPosition = new Vector2(Info.gameWidth / 2, (int)(Info.gameHeight * .7f));
            Vector2 aboutTextPosition = new Vector2(Info.gameWidth / 2, (int)(Info.gameHeight * .8f));

            menuItems.Add(new MenuItem("Start", startTextPosition - (startTextSize / 2), ScreenManager.Fonts["Menu Item"]));
            menuItems.Add(new MenuItem("Options", optionsTextPosition - (optionsTextSize / 2), ScreenManager.Fonts["Menu Item"]));
            menuItems.Add(new MenuItem("About", aboutTextPosition - (aboutTextSize / 2), ScreenManager.Fonts["Menu Item"]));

            InputManager.Register(Keys.Down, OnDownPressed);
            InputManager.Register(Keys.Enter, OnEnterPressed);
            InputManager.Register(Keys.Up, OnUpPressed);
            foreach (MenuItem item in menuItems) {
                InputManager.Register(MouseButtons.LeftButton, item.GetBounds(), OnEnterPressed);
            }
            base.LoadAssets();
        }

        public override void UnloadAssets() {
            InputManager.Unregister(Keys.Down, OnDownPressed);
            InputManager.Unregister(Keys.Enter, OnEnterPressed);
            InputManager.Unregister(Keys.Up, OnUpPressed);
            foreach (MenuItem item in menuItems) {
                InputManager.Unregister(MouseButtons.LeftButton, item.GetBounds(), OnEnterPressed);
            }

            ScreenManager.RemoveFont("Title");
            ScreenManager.RemoveFont("Menu Item");
            base.UnloadAssets();
        }

        public override void Draw(Microsoft.Xna.Framework.GameTime gameTime) {
            spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.NonPremultiplied);
            DrawTitle(spriteBatch);
            DrawMenuItems(spriteBatch, gameTime);
            spriteBatch.End();

            base.Draw(gameTime);
        }

        private void DrawTitle(SpriteBatch spriteBatch) {
            Vector2 textSize = ScreenManager.Fonts["Title"].MeasureString("PONG");
            Vector2 textCenter = new Vector2(Info.gameWidth / 2, (int)(Info.gameHeight * .2f));
            spriteBatch.DrawString(ScreenManager.Fonts["Title"], "PONG", textCenter - (textSize / 2), Microsoft.Xna.Framework.Color.White);
        }
        private void DrawMenuItems(SpriteBatch spriteBatch, GameTime gameTime) {
            for (int i = 0; i < menuItems.Count; i++) {
                MenuItem item = menuItems[i];
                item.Draw(this, true ? selectedIndex == i : false, gameTime);
            }

        }

        public void OnEnterPressed() {
            switch (SelectedIndex) {
                case 0:
                    ScreenManager.ChangeScreens(this, new GameScreen());
                    break;
                default:
                    return;
            }
        }

        public void OnDownPressed() {
            SelectedIndex++;
        }

        public void OnUpPressed() {
            SelectedIndex--;
        }

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime) {
            if (Mouse.GetState().RightButton == ButtonState.Pressed) {
                ScreenManager.ChangeScreens(this, new ErrorScreen());
            }

            int hitIndex = GetIndexOfHitMenuItem(Mouse.GetState());
            if (hitIndex >= 0) {
                selectedIndex = hitIndex;
            }

            base.Update(gameTime);
        }

        private int GetIndexOfHitMenuItem(MouseState mouseState) {
            for (int i = 0; i < menuItems.Count; i++) {
                MenuItem item = menuItems[i];
                if (item.GetBounds().Contains(new Point(mouseState.X, mouseState.Y))) {
                    return i;
                }
            }
            return -1;
        }
    }
}
