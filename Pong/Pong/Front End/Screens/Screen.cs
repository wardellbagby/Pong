using Microsoft.Xna.Framework;
using Pong.Front_End.Managers;

namespace Pong.Front_End.Screens {
    
    //Base class for all Screens. Intentionally left blank/not abstract.
    public class Screen {

        public Color BackgroundColor = Color.CornflowerBlue;
        protected InputManager InputManager = ScreenManager.InputManager;

        public virtual void LoadAssets() { }

        public virtual void Update(GameTime gameTime) { }

        public virtual void Draw(GameTime gameTime) { }

        public virtual void UnloadAssets() { }
    }
}