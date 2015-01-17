using System;
using System.Collections.Generic;
using Pong.Front_End.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Pong.Front_End.Managers {

    public class ScreenManager : Game {
        public static GraphicsDeviceManager GraphicsDeviceManager;
        public static SpriteBatch Sprites;
        public static Dictionary<string, Texture2D> Textures2D;
        public static Dictionary<string, Texture3D> Textures3D;
        public static Dictionary<string, SpriteFont> Fonts;
        public static Dictionary<string, Model> Models;
        public static List<Screen> ScreenList;
        public static ContentManager ContentManager;
        public static InputManager InputManager;
        private bool toggleFullScreen;
        private static string FontFolder = "Fonts";

        public ScreenManager() {
            InputManager = new InputManager();
            GraphicsDeviceManager = new GraphicsDeviceManager(this);

            GraphicsDeviceManager.PreferredBackBufferWidth = Pong.Back_End.GameInfo.gameWidth;
            GraphicsDeviceManager.PreferredBackBufferHeight = Pong.Back_End.GameInfo.gameHeight;

            GraphicsDeviceManager.IsFullScreen = false;
            IsMouseVisible = true;

            Content.RootDirectory = "Content";
        }

        protected override void Initialize() {
            Textures2D = new Dictionary<string, Texture2D>();
            Textures3D = new Dictionary<string, Texture3D>();
            Models = new Dictionary<string, Model>();
            Fonts = new Dictionary<string, SpriteFont>();

            InputManager.Register(Keys.Escape, () => Exit());
            base.Initialize();
        }
        protected override void LoadContent() {
            ContentManager = Content;
            Sprites = new SpriteBatch(GraphicsDevice);
            AddFont("Default");

            AddScreen(new MainMenuScreen());
        }
        protected override void UnloadContent() {
            foreach (var screen in ScreenList) {
                screen.UnloadAssets();
            }
            Textures2D.Clear();
            Textures3D.Clear();
            Fonts.Clear();
            Models.Clear();
            ScreenList.Clear();
            Content.Unload();
            RemoveFont("Default");
        }
        protected override void Update(GameTime gameTime) {
            InputManager.Update(Keyboard.GetState(), Mouse.GetState());

            var startIndex = ScreenList.Count - 1;
            for (var i = startIndex; i < ScreenList.Count; i++) {
                ScreenList[i].Update(gameTime);
            }
            //InputManager doesn't currently support key combos.
            if (Keyboard.GetState().IsKeyDown(Keys.F11) || ((Keyboard.GetState().IsKeyDown(Keys.LeftAlt) || Keyboard.GetState().IsKeyDown(Keys.RightAlt)) && Keyboard.GetState().IsKeyDown(Keys.Enter))) {
                toggleFullScreen = true;
            }
            base.Update(gameTime);

        }
        protected override void Draw(GameTime gameTime) {
            if (toggleFullScreen) {
                GraphicsDeviceManager.ToggleFullScreen();
                toggleFullScreen = false;
            }
            var startIndex = ScreenList.Count - 1;

            GraphicsDevice.Clear(ScreenList[startIndex].BackgroundColor);
            GraphicsDeviceManager.GraphicsDevice.Clear(ScreenList[startIndex].BackgroundColor);

            for (var i = startIndex; i < ScreenList.Count; i++) {
                ScreenList[i].Draw(gameTime);
            }


            base.Draw(gameTime);
        }
        public static void AddFont(string fontName) {
            if (Fonts == null) {
                Fonts = new Dictionary<string, SpriteFont>();
            }
            if (!Fonts.ContainsKey(fontName)) {
                Fonts.Add(fontName, ContentManager.Load<SpriteFont>(FontFolder + '\\' + fontName));
            }
        }

        public static void RemoveFont(string fontName) {
            if (Fonts.ContainsKey(fontName)) {
                Fonts.Remove(fontName);
            }
        }

        public static void AddTexture2D(string textureName) {
            if (Textures2D == null) {
                Textures2D = new Dictionary<string, Texture2D>();
            }
            if (!Textures2D.ContainsKey(textureName)) {
                Textures2D.Add(textureName, ContentManager.Load<Texture2D>(textureName));
            }
        }

        public static void RemoveTexture2D(string textureName) {
            if (Textures2D.ContainsKey(textureName)) {
                Textures2D.Remove(textureName);
            }
        }

        public static void AddTexture3D(string textureName) {
            if (Textures3D == null) {
                Textures3D = new Dictionary<string, Texture3D>();
            }
            if (!Textures3D.ContainsKey(textureName)) {
                Textures3D.Add(textureName, ContentManager.Load<Texture3D>(textureName));
            }
        }

        public static void RemoveTexture3D(string textureName) {
            if (Textures3D.ContainsKey(textureName)) {
                Textures3D.Remove(textureName);
            }
        }

        public static void AddModel(string modelName) {
            if (Models == null) {
                Models = new Dictionary<string, Model>();
            }
            if (!Models.ContainsKey(modelName)) {
                Models.Add(modelName, ContentManager.Load<Model>(modelName));
            }
        }

        public static void RemoveModel(string modelName) {
            if (Models.ContainsKey(modelName)) {
                Models.Remove(modelName);
            }
        }
        public static void AddScreen(Screen gameScreen) {
            if (ScreenList == null) {
                ScreenList = new List<Screen>();
            }
            ScreenList.Add(gameScreen);
            gameScreen.LoadAssets();
        }

        public static void RemoveScreen(Screen gameScreen) {
            gameScreen.UnloadAssets();
            ScreenList.Remove(gameScreen);
            if (ScreenList.Count < 1)
                AddScreen(new ErrorScreen());
        }

        public static void ChangeScreens(Screen currentScreen, Screen targetScreen) {
            RemoveScreen(currentScreen);
            AddScreen(targetScreen);
        }
    }
}
