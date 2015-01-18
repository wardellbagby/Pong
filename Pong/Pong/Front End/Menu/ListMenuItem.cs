using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Pong.Front_End.Managers;
using Pong.Front_End.Screens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pong.Front_End.Menu {
    class ListMenuItem : MenuItem {
        private string[] options;
        private int selectedOption = 0;
        private Vector2 screenSize = Vector2.Zero;
        private float yPosition;

        public string[] Options {
            get { return options; }
            set {
                if (value == null)
                    throw new ArgumentNullException();
                if (value.Length == 0) {
                    throw new ArgumentException("Must have at least one option.");
                }
                options = value;
            }
        }

        private int SelectedOption {
            get { return selectedOption; }
            set {
                if (value < 0) {
                    selectedOption = Options.Length - 1;
                } else if (value > Options.Length - 1) {
                    selectedOption = 0;
                } else {
                    selectedOption = value;
                }
            }
        }
        public ListMenuItem(string text, string[] options, float yPosition, SpriteFont font, Vector2 screenSize)
            : base(text, yPosition, font, screenSize) {
            Options = options;
            this.screenSize = screenSize;
            this.yPosition = yPosition;
            Vector2 textSize = Font.MeasureString(Text + Options[SelectedOption]);
            Vector2 textPosition = new Vector2(screenSize.X/2, (int)(screenSize.Y * (yPosition / screenSize.Y)));
            Position = textPosition - (textSize / 2);

        }

        public ListMenuItem(string label, string[] options, Vector2 position, SpriteFont font)
            : base(label, position, font) {
            Options = options;
        }

        public override void Draw(Screen screen, bool isSelected, GameTime gameTime) {
            Color color = isSelected ? SelectedColor : UnselectedColor;

            double time = gameTime.TotalGameTime.TotalSeconds;
            float pulsate = (float)Math.Sin(time * 6) + 1;
            int fade = isSelected ? (int)(63 * Math.Sin(time * 6) + 192) : 255;
            color.A = (byte)fade;

            SpriteBatch spriteBatch = ScreenManager.Sprites;
            Vector2 origin = new Vector2(0, Font.LineSpacing / 2);
            string displayedText = Text + options[selectedOption];

            spriteBatch.DrawString(Font, displayedText, Position, color);
        }

        public override int GetWidth() {
            if (options == null) {
                return base.GetWidth();
            }
            return (int)Font.MeasureString(Text + options[selectedOption]).X;
        }

        public void NextOption() {
            SelectedOption++;
            if (screenSize != Vector2.Zero) {
                Vector2 textSize = Font.MeasureString(Text + Options[SelectedOption]);
                Vector2 textPosition = new Vector2(screenSize.X/2, (int)(screenSize.Y * (yPosition / screenSize.Y)));
                Position = textPosition - (textSize / 2);
            }
        }
        public void PreviousOption() {
            SelectedOption--;
            if (screenSize != Vector2.Zero) {
                Vector2 textSize = Font.MeasureString(Text + Options[SelectedOption]);
                Vector2 textPosition = new Vector2(screenSize.X/2, (int)(screenSize.Y * (yPosition / screenSize.Y)));
                Position = textPosition - (textSize / 2);
            }
        }
    }
}
