using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Pong.Front_End.Managers;
using Pong.Front_End.Screens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pong.Front_End
{

    class MenuItem
    {
        private string text;
        private Vector2 position;
        private Color selectedColor;
        private Color unselectedColor;

        public Color SelectedColor
        {
            get { return selectedColor; }
            set { selectedColor = value; }
        }

        public Color UnselectedColor
        {
            get { return unselectedColor; }
            set { unselectedColor = value; }
        }

        public string Text
        {
            get { return text; }
            set { text = value; }
        }
        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }

        public MenuItem(string text, Vector2 position)
        {
            this.text = text;
            this.position = position;
            selectedColor = Color.Gold;
            unselectedColor = Color.White;
        }


        public virtual void Draw(GameScreen screen, bool isSelected, GameTime gameTime)
        {
            Color color = isSelected ? selectedColor : unselectedColor;

            double time = gameTime.TotalGameTime.TotalSeconds;
            float pulsate = (float)Math.Sin(time * 6) + 1;
            int fade = isSelected ? (int)(63 * Math.Sin(time * 6) + 192) : 255;
            color.A = (byte)fade;
            //float scale = isSelected ? 1 + pulsate * 0.05f : 1;


            SpriteBatch spriteBatch = ScreenManager.Sprites;
            SpriteFont font = ScreenManager.Fonts["Menu Item"];
            Vector2 origin = new Vector2(0, font.LineSpacing / 2);

            spriteBatch.DrawString(font, text, position, color);
        }

        public virtual int GetHeight()
        {
            return ScreenManager.Fonts["Menu Item"].LineSpacing;
        }

        public virtual int GetWidth()
        {
            return (int)ScreenManager.Fonts["Menu Item"].MeasureString(Text).X;
        }
    }
}


