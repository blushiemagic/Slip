using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Slip.Puzzles
{
    public class LifeCapsule : Puzzle
    {
        public const float width = 40f;
        public const float height = 20f;
        public static Texture2D texture;

        public override void Draw(GameScreen screen, int x, int y, Main main)
        {
            main.spriteBatch.Draw(texture, screen.DrawPos(main, Room.TileToWorldPos(new Vector2(x + 0.5f, y + 0.5f))),
                null, Color.White, texture.Center());
        }

        public override void OnPlayerCollide(Room room, int x, int y, Player player)
        {
            Vector2 pos = Tile.tileSize * new Vector2(x + 0.5f, y + 0.5f);
            if (Math.Abs(player.position.X - pos.X) <= (Player.size + width) / 2f &&
                Math.Abs(player.position.Y - pos.Y) <= (Player.size + height) / 2f)
            {
                player.maxLife++;
                player.life++;
                room.RemovePuzzle(x, y);
            }
        }

        public static void LoadContent(ContentManager loader)
        {
            texture = loader.Load<Texture2D>("LifeCapsule");
        }
    }
}
