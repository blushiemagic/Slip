using System;
using Microsoft.Xna.Framework;

namespace Slip
{
    public class WinScreen : Screen
    {
        private string levelName;
        private Color levelColor;

        public WinScreen(string name, Color color)
        {
            levelName = name;
            levelColor = color;
        }

        public override void Initialize(Main main)
        {
            base.Initialize(main);
            Text text = new Text("Congratulations!", Color.White, Color.Black);
            text.alignment = new Vector2(0.5f, 0.25f);
            AddComponent(text);
            text = new Text("You have completed", Color.White, Color.Black);
            text.alignment = new Vector2(0.5f, 0.5f);
            text.position = new Vector2(0f, -20f);
            AddComponent(text);
            text = new Text(levelName, Color.White, Color.Black);
            text.alignment = new Vector2(0.5f, 0.5f);
            text.position = new Vector2(0f, 20f);
            AddComponent(text);
            Panel panel = new TextPanel("Continue", Color.Black);
            panel.alignment = new Vector2(0.5f, 0.75f);
            panel.size = new Vector2(300f, 50f);
            panel.OnClick += (m => m.changeScreen = new MainMenu());
            AddComponent(panel);
        }

        public override Color BackgroundColor()
        {
            return levelColor;
        }
    }
}
