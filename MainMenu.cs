using System;
using Microsoft.Xna.Framework;
using Slip.Levels;

namespace Slip
{
    public class MainMenu : Screen
    {
        public override void Initialize(Main main)
        {
            base.Initialize(main);
            Panel panel = new TextPanel("Testing", Color.Black);
            panel.alignment = new Vector2(0.5f, 0.5f);
            panel.size = new Vector2(300f, 50f);
            panel.OnClick += StartDemo;
            AddComponent(panel);
        }

        private static void StartDemo(Main main)
        {
            main.changeScreen = new Tutorial();
        }
    }
}
