using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Pong.Front_End.Managers;
using Pong.Front_End.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Info = Pong.Back_End.GameInfo;

namespace Pong.Front_End.Screens {
    class OptionsScreen : Screen {
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
            ScreenManager.AddFont("Menu Item");

            Vector2 startTextSize = ScreenManager.Fonts["Menu Item"].MeasureString("Difficulty: ");
            Vector2 optionsTextSize = ScreenManager.Fonts["Menu Item"].MeasureString("Name: ");
            Vector2 aboutTextSize = ScreenManager.Fonts["Menu Item"].MeasureString("Player Count: ");
            Vector2 startTextPosition = new Vector2(Info.gameWidth / 2, (int)(Info.gameHeight * .1f));
            Vector2 optionsTextPosition = new Vector2(Info.gameWidth / 2, (int)(Info.gameHeight * .2f));
            Vector2 aboutTextPosition = new Vector2(Info.gameWidth / 2, (int)(Info.gameHeight * .3f));

            menuItems.Add(new ListMenuItem("Difficulty: ", new string[] { "Easy", "Medium", "Never Pick This Option" }, Info.gameHeight * .1f, ScreenManager.Fonts["Menu Item"], new Vector2(Info.gameWidth, Info.gameHeight)));
            menuItems.Add(new ListMenuItem("Name: ", new string[] { "Dummy", "Smart", "Jimmy Neutron: Boy Genius" }, Info.gameHeight * .2f, ScreenManager.Fonts["Menu Item"], new Vector2(Info.gameWidth, Info.gameHeight)));
            menuItems.Add(new ListMenuItem("Player Count: ", new string[] { "1", "2", "5,000,000,000,000" }, Info.gameHeight * .3f, ScreenManager.Fonts["Menu Item"], new Vector2(Info.gameWidth, Info.gameHeight)));

            InputManager.Register(Keys.Down, OnDownPressed);
            InputManager.Register(Keys.Enter, OnEnterPressed);
            InputManager.Register(Keys.Up, OnUpPressed);
            InputManager.Register(Keys.Left, OnLeftPressed);
            InputManager.Register(Keys.Right, OnRightPressed);
            InputManager.Register(Keys.Escape, OnEscapePressed);
            foreach (MenuItem item in menuItems) {
                InputManager.Register(MouseButtons.LeftButton, item.GetBounds(), OnRightPressed);
            }
            base.LoadAssets();
        }

        public override void UnloadAssets() {
            InputManager.Unregister(Keys.Down, OnDownPressed);
            InputManager.Unregister(Keys.Enter, OnEnterPressed);
            InputManager.Unregister(Keys.Up, OnUpPressed);
            InputManager.Unregister(Keys.Left, OnLeftPressed);
            InputManager.Unregister(Keys.Right, OnRightPressed);
            InputManager.Unregister(Keys.Escape, OnEscapePressed);

            foreach (MenuItem item in menuItems) {
                InputManager.Unregister(MouseButtons.LeftButton, item.GetBounds(), OnEnterPressed);
            }

            ScreenManager.RemoveFont("Menu Item");
            base.UnloadAssets();
        }

        public override void Draw(Microsoft.Xna.Framework.GameTime gameTime) {
            spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.NonPremultiplied);
            DrawMenuItems(spriteBatch, gameTime);
            spriteBatch.End();

            base.Draw(gameTime);
        }

        private void DrawMenuItems(SpriteBatch spriteBatch, GameTime gameTime) {
            for (int i = 0; i < menuItems.Count; i++) {
                MenuItem item = menuItems[i];
                item.Draw(this, true ? selectedIndex == i : false, gameTime);
            }

        }

        public void OnEnterPressed() {

        }

        public void OnDownPressed() {
            SelectedIndex++;
        }

        public void OnUpPressed() {
            SelectedIndex--;
        }
        public void OnLeftPressed() {
            if (menuItems[selectedIndex] is ListMenuItem) {
                ((ListMenuItem)(menuItems[SelectedIndex])).PreviousOption();
            }
        }
        public void OnRightPressed() {
            if (menuItems[selectedIndex] is ListMenuItem) {
                ((ListMenuItem)(menuItems[SelectedIndex])).NextOption();
            }
        }

        public void OnEscapePressed() {
            ScreenManager.ChangeScreens(this, new MainMenuScreen());
        }

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime) {
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

