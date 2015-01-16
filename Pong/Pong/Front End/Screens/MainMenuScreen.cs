using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Manager = Pong.Front_End.Managers.ScreenManager;
using Info = Pong.Back_End.GameInfo;
using Microsoft.Xna.Framework.Graphics;

namespace Pong.Front_End.Screens
{
    class MainMenuScreen : Screen
    {
        private SpriteBatch spriteBatch;
        private List<MenuItem> menuItems = new List<MenuItem>();
        private int selectedIndex = 0;
        private Dictionary<Keys, ButtonState> previousButtonStates;

        private int SelectedIndex
        {
            get { return selectedIndex; }
            set { selectedIndex = value >= menuItems.Count ? 0 : value < 0 ? menuItems.Count - 1 : value; }
        }

        public override void LoadAssets()
        {
            previousButtonStates = new Dictionary<Keys, ButtonState>();
            spriteBatch = Manager.Sprites;
            BackgroundColor = Color.Black;
            Manager.AddFont("Title");
            Manager.AddFont("Menu Item");

            Vector2 startTextSize = Manager.Fonts["Menu Item"].MeasureString("Start");
            Vector2 optionsTextSize = Manager.Fonts["Menu Item"].MeasureString("Options");
            Vector2 aboutTextSize = Manager.Fonts["Menu Item"].MeasureString("About");
            Vector2 startTextPosition = new Vector2(Info.gameWidth / 2, (int)(Info.gameHeight * .6f));
            Vector2 optionsTextPosition = new Vector2(Info.gameWidth / 2, (int)(Info.gameHeight * .7f));
            Vector2 aboutTextPosition = new Vector2(Info.gameWidth / 2, (int)(Info.gameHeight * .8f));

            menuItems.Add(new MenuItem("Start", startTextPosition - (startTextSize / 2)));
            menuItems.Add(new MenuItem("Options", optionsTextPosition - (optionsTextSize / 2)));
            menuItems.Add(new MenuItem("About", aboutTextPosition - (aboutTextSize / 2)));
            base.LoadAssets();
        }

        public override void UnloadAssets()
        {
            Manager.RemoveFont("Title");
            Manager.RemoveFont("Menu Item");
            base.UnloadAssets();
        }

        public override void Draw(Microsoft.Xna.Framework.GameTime gameTime)
        {
            spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.NonPremultiplied);
            DrawTitle(spriteBatch);
            DrawMenuItems(spriteBatch, gameTime);
            spriteBatch.End();

            base.Draw(gameTime);
        }

        private void DrawTitle(SpriteBatch spriteBatch)
        {
            Vector2 textSize = Manager.Fonts["Title"].MeasureString("PONG");
            Vector2 textCenter = new Vector2(Info.gameWidth / 2, (int)(Info.gameHeight * .2f));
            spriteBatch.DrawString(Manager.Fonts["Title"], "PONG", textCenter - (textSize / 2), Microsoft.Xna.Framework.Color.White);
        }
        private void DrawMenuItems(SpriteBatch spriteBatch, GameTime gameTime)
        {
            for (int i = 0; i < menuItems.Count; i++)
            {
                MenuItem item = menuItems[i];
                item.Draw(this, true ? selectedIndex == i : false, gameTime);
            }

        }

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            //TODO Wardell seriously needs to refactor this into an InputManager before it becomes unwieldly.
            if (Mouse.GetState().RightButton == ButtonState.Pressed)
            {
                Manager.ChangeScreens(this, new ErrorScreen());
            }
            foreach (Keys item in Keyboard.GetState().GetPressedKeys())
            {
                if (!previousButtonStates.ContainsKey(item))
                    previousButtonStates.Add(item, ButtonState.Pressed);
                else
                    previousButtonStates[item] = ButtonState.Pressed;
            }
            if (Keyboard.GetState().IsKeyUp(Keys.Enter) && (previousButtonStates.ContainsKey(Keys.Enter) && previousButtonStates[Keys.Enter] == ButtonState.Pressed) && selectedIndex == 0)
            {
                Manager.ChangeScreens(this, new GameScreen());
            }
            if (Keyboard.GetState().IsKeyUp(Keys.Down) && (previousButtonStates.ContainsKey(Keys.Down) && previousButtonStates[Keys.Down] == ButtonState.Pressed))
            {
                SelectedIndex++;
                previousButtonStates[Keys.Down] = ButtonState.Released;

            }
            else if (Keyboard.GetState().IsKeyUp(Keys.Up) && (previousButtonStates.ContainsKey(Keys.Up) && previousButtonStates[Keys.Up] == ButtonState.Pressed))
            {
                SelectedIndex--;
                previousButtonStates[Keys.Up] = ButtonState.Released;

            }
            base.Update(gameTime);
        }
    }
}
