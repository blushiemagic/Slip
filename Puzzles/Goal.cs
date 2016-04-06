using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Slip.Particles;

namespace Slip.Puzzles
{
    public class Goal : Puzzle
    {
        private string levelName;
        private Color levelColor;

        public Goal(string name, Color color)
        {
            levelName = name;
            levelColor = color;
        }

        public override void Update(Room room, int x, int y, Player player)
        {
            Vector2 pos = new Vector2(x + (float)Main.rand.NextDouble(), y + (float)Main.rand.NextDouble());
            Color color = Main.rand.Next(2) == 0 ? Color.Fuchsia : Color.Orchid;
            room.particles.Add(new Sparkle(pos * Tile.tileSize, color));
        }

        public override void OnPlayerCollide(Room room, int x, int y, Player player)
        {
            Main.instance.changeScreen = new WinScreen(levelName, levelColor);
        }

        public static void LoadContent(ContentManager loader)
        {
            Sparkle.LoadContent(loader);
        }
    }
}
