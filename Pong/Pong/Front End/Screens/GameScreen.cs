using Microsoft.Xna.Framework;
using Manager = Pong.Front_End.ScreenManager.ScreenManager;

namespace Pong.Front_End.Screens
{
    public class GameScreen
    {
        public bool IsActive = true;
        public bool IsPopup = false;
        public Color BackgroundColor = Color.CornflowerBlue;

        public virtual void LoadAssets()
        {
            Manager.AddFont("Default Font");
        }
        public virtual void Update(GameTime gameTime) { }
        public virtual void Draw(GameTime gameTime) { }
        public virtual void UnloadAssets() { }
    }
}