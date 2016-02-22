using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Slip
{
    public class Main : Game
    {
        internal static Main instance;
        public static Random rand = new Random();

        internal GraphicsDeviceManager graphics;
        internal SpriteBatch spriteBatch;

        private Screen screen;
        public Screen changeScreen = null;
        public Point mousePos;
        public Point mouseMove;
        public bool leftMouseClick;
        public bool rightMouseClick;
        public bool middleMouseClick;
        public bool leftMousePress;
        public bool rightMousePress;
        public bool middleMousePress;
        public bool leftMouseRelease;
        public bool rightMouseRelease;
        public bool middleMouseRelease;
        public int mouseWheel;
        public int mouseScrollSpeed;

        public Main()
        {
            instance = this;
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 1000;
            graphics.PreferredBackBufferHeight = 600;
            this.IsMouseVisible = true;
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            base.Initialize();
            ChangeScreen(new MainMenu());
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        protected override void UnloadContent()
        {
            Content.Unload();
        }
        
        protected override void Update(GameTime gameTime)
        {
            if (IsKeyPressed(Keys.Escape))
            {
                base.Exit();
                return;
            }
            UpdateMouseState();
            screen.Update(this);
            if (changeScreen != null)
            {
                ChangeScreen(changeScreen);
                changeScreen = null;
            }
            base.Update(gameTime);
        }

        public static bool IsKeyPressed(Keys key)
        {
            return Keyboard.GetState().IsKeyDown(key);
        }

        private void UpdateMouseState()
        {
            mouseMove = Mouse.GetState().Position - mousePos;
            mousePos = Mouse.GetState().Position;
            leftMouseClick = !leftMousePress && Mouse.GetState().LeftButton == ButtonState.Pressed;
            leftMouseRelease = leftMousePress && Mouse.GetState().LeftButton == ButtonState.Released;
            leftMousePress = Mouse.GetState().LeftButton == ButtonState.Pressed;
            rightMouseClick = !rightMousePress && Mouse.GetState().RightButton == ButtonState.Pressed;
            rightMouseRelease = rightMousePress && Mouse.GetState().RightButton == ButtonState.Released;
            rightMousePress = Mouse.GetState().RightButton == ButtonState.Pressed;
            middleMouseClick = !middleMousePress && Mouse.GetState().MiddleButton == ButtonState.Pressed;
            middleMouseRelease = middleMousePress && Mouse.GetState().MiddleButton == ButtonState.Released;
            middleMousePress = Mouse.GetState().MiddleButton == ButtonState.Pressed;
            mouseScrollSpeed = Mouse.GetState().ScrollWheelValue - mouseWheel;
            mouseWheel = Mouse.GetState().ScrollWheelValue;
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(screen.BackgroundColor());
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointWrap);
            screen.Draw(this);
            spriteBatch.End();
            base.Draw(gameTime);
        }

        public Vector2 Size()
        {
            return new Vector2(GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);
        }

        public Vector2 Center()
        {
            return Size() / 2f;
        }

        private void ChangeScreen(Screen newScreen)
        {
            Content.Unload();
            Textures.LoadContent(Content);
            screen = newScreen;
            newScreen.LoadContent(Content);
            newScreen.Initialize(this);
        }
    }
}
