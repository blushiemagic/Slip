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
        private static Keys[] controlsToKeys = new Keys[(int)KeyControl.Count];
        private static bool[] controlPress = new bool[(int)KeyControl.Count];
        private static bool[] controlHold = new bool[(int)KeyControl.Count];
        private static bool[] controlRelease = new bool[(int)KeyControl.Count];
        public static Point mousePos;
        public static Point mouseMove;
        public static bool leftMouseClick;
        public static bool rightMouseClick;
        public static bool middleMouseClick;
        public static bool leftMousePress;
        public static bool rightMousePress;
        public static bool middleMousePress;
        public static bool leftMouseRelease;
        public static bool rightMouseRelease;
        public static bool middleMouseRelease;
        public static int mouseWheel;
        public static int mouseScrollSpeed;

        public Main()
        {
            instance = this;
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 1000;
            graphics.PreferredBackBufferHeight = 600;
            this.IsMouseVisible = true;
            Content.RootDirectory = "Content";
            SetupDefaultControls();
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
            if (IsControlPressed(KeyControl.Escape))
            {
                if (screen is MainMenu)
                {
                    base.Exit();
                    return;
                }
                ChangeScreen(new MainMenu());
            }
            UpdateKeyState();
            UpdateMouseState();
            screen.Update(this);
            if (changeScreen != null)
            {
                ChangeScreen(changeScreen);
                changeScreen = null;
            }
            base.Update(gameTime);
        }

        private static void SetupDefaultControls()
        {
            MapKey(KeyControl.Escape, Keys.Escape);
            MapKey(KeyControl.Up, Keys.Up);
            MapKey(KeyControl.Left, Keys.Left);
            MapKey(KeyControl.Down, Keys.Down);
            MapKey(KeyControl.Right, Keys.Right);
            MapKey(KeyControl.Action, Keys.Z);
            MapKey(KeyControl.Focus, Keys.LeftShift);
            MapKey(KeyControl.Debug, Keys.I);
        }

        public static void MapKey(KeyControl control, Keys key)
        {
            controlsToKeys[(int)control] = key;
        }

        private static void UpdateKeyState()
        {
            for (int k = 0; k < (int)KeyControl.Count; k++)
            {
                bool keyDown = Keyboard.GetState().IsKeyDown(controlsToKeys[k]);
                bool keyUp = Keyboard.GetState().IsKeyUp(controlsToKeys[k]);
                controlPress[k] = !controlHold[k] && keyDown;
                controlRelease[k] = controlHold[k] && keyUp;
                controlHold[k] = keyDown;
            }
        }

        public static bool IsControlPressed(KeyControl control)
        {
            return controlPress[(int)control];
        }

        public static bool IsControlHeld(KeyControl control)
        {
            return controlHold[(int)control];
        }

        public static bool IsControlReleased(KeyControl control)
        {
            return controlRelease[(int)control];
        }

        private static void UpdateMouseState()
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
