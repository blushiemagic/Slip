using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Slip.Puzzles
{
    public class Lava : Puzzle
    {
        public static Texture2D texture;

        public override void Draw(GameScreen screen, int x, int y, Main main)
        {
            main.spriteBatch.Draw(texture, screen.DrawPos(main, Room.TileToWorldPos(x, y)), null, Color.White);
        }

        public override void OnPlayerCollide(Room room, int x, int y, Player player)
        {
            player.TakeDamage(10);
        }

        public static void LoadContent(ContentManager loader)
        {
            texture = loader.Load<Texture2D>("Lava");
        }
    }
}
