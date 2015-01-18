using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Pong.Front_End.Managers;
using Pong.Front_End.Screens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pong.Front_End.Menu {

    class MenuItem {
        private string text;
        private Vector2 position;
        private Color selectedColor;
        private Color unselectedColor;
        private SpriteFont font;
        private Rectangle bounds;

        public SpriteFont Font {
            get { return font; }
            set {
                if (value == null) {
                    throw new ArgumentNullException("Font cannot be null");
                }
                font = value;
                if (position != null)
                    bounds = new Rectangle((int)(position.X), (int)(position.Y), GetWidth(), GetHeight());
                else {
                    bounds = Rectangle.Empty;
                }
            }
        }

        public Color SelectedColor {
            get { return selectedColor; }
            set { selectedColor = value; }
        }

        public Color UnselectedColor {
            get { return unselectedColor; }
            set { unselectedColor = value; }
        }

        public string Text {
            get { return text; }
            set { text = value; }
        }
        public Vector2 Position {
            get { return position; }
            set {
                if (value == null) {
                    throw new ArgumentNullException("Position cannot be null");
                }
                position = value;
                if (font != null)
                    bounds = new Rectangle((int)(position.X), (int)(position.Y), GetWidth(), GetHeight());
                else {
                    bounds = Rectangle.Empty;
                }
            }
        }
        public MenuItem(string text, float yPosition, SpriteFont font, Vector2 screenSize) {
            if (font == null) {
                throw new ArgumentNullException("Font cannot be null.");
            }
            Vector2 textSize = font.MeasureString(text);          
            Vector2 textPosition = new Vector2(screenSize.X, (int)(screenSize.Y * (yPosition/screenSize.Y)));
            this.position = textPosition - (textSize / 2);
            this.text = text;
            this.font = font;
            selectedColor = Color.Gold;
            unselectedColor = Color.White;
        }

        public MenuItem(string text, Vector2 position, SpriteFont font) {
            if (position == null || font == null) {
                throw new ArgumentNullException("Position and/or Font cannot be null.");
            }
            this.Text = text;
            this.Font = font;
            this.Position = position;
            selectedColor = Color.Gold;
            unselectedColor = Color.White;
        }

        public Rectangle GetBounds() {
            return bounds;
        }
        public virtual void Draw(Screen screen, bool isSelected, GameTime gameTime) {
            Color color = isSelected ? selectedColor : unselectedColor;

            double time = gameTime.TotalGameTime.TotalSeconds;
            float pulsate = (float)Math.Sin(time * 6) + 1;
            int fade = isSelected ? (int)(63 * Math.Sin(time * 6) + 192) : 255;
            color.A = (byte)fade;

            SpriteBatch spriteBatch = ScreenManager.Sprites;
            Vector2 origin = new Vector2(0, font.LineSpacing / 2);

            spriteBatch.DrawString(font, text, position, color);
        }

        public virtual int GetHeight() {
            return font.LineSpacing;
        }

        public virtual int GetWidth() {
            return (int)font.MeasureString(Text).X;
        }
    }
}


