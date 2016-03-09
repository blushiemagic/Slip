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
            Panel panel = new TextPanel("Tutorial", Color.Black);
            panel.alignment = new Vector2(0.5f, 0.5f);
            panel.position = new Vector2(0f, -70f);
            panel.size = new Vector2(300f, 50f);
            panel.OnClick += (m => m.changeScreen = new Tutorial());
            AddComponent(panel);
            panel = new TextPanel("Level 1", Color.Black);
            panel.alignment = new Vector2(0.5f, 0.5f);
            panel.size = new Vector2(300f, 50f);
            panel.OnClick += (m => m.changeScreen = new FirstDungeon());
            AddComponent(panel);
            panel = new TextPanel("Demo", Color.Black);
            panel.alignment = new Vector2(0.5f, 0.5f);
            panel.position = new Vector2(0f, 70f);
            panel.size = new Vector2(300f, 50f);
            panel.OnClick += (m => m.changeScreen = new DemoScreen());
            AddComponent(panel);
        }
    }
}
