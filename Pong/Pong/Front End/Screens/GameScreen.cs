using Microsoft.Xna.Framework;
using Pong.Front_End.Managers;

namespace Pong.Front_End.Screens
{
    public class GameScreen
    {
        public bool IsActive = true;
        public bool IsPopup = false;
        public Color BackgroundColor = Color.CornflowerBlue;

        public virtual void LoadAssets()
        {
            ScreenManager.AddFont("Default");
        }
        public virtual void Update(GameTime gameTime) { }
        public virtual void Draw(GameTime gameTime) { }
        public virtual void UnloadAssets() { }
    }
}