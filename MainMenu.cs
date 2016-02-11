using System;
using Microsoft.Xna.Framework;

namespace Slip
{
    public class MainMenu : Screen
    {
        public override void Initialize(Main main)
        {
            base.Initialize(main);
            Panel panel = new Panel();
            panel.alignment = new Vector2(0.5f, 0.5f);
            panel.size = new Vector2(200f, 40f);
            panel.OnClick += StartDemo;
            AddComponent(panel);
        }

        private static void StartDemo(Main main)
        {
            main.changeScreen = new GameScreen();
        }
    }
}
