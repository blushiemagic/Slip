using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Slip.Puzzles
{
    public class Lava : Puzzle
    {
        public static Texture2D texture;
        public static float timer = 0f;

        public static void IncrementTimer()
        {
            timer += 0.25f;
            if (timer >= Tile.tileSize)
            {
                timer -= Tile.tileSize;
            }
        }

        public override void Draw(GameScreen screen, int x, int y, Main main)
        {
            int offset = (int)Math.Floor(timer);
            Vector2 pos = Room.TileToWorldPos(x, y);
            Vector2 offsetPos = new Vector2(0f, offset);
            Rectangle source = new Rectangle(offset, 0, Tile.tileSize - offset, Tile.tileSize - offset);
            main.spriteBatch.Draw(texture, screen.DrawPos(main, pos + offsetPos), source, Color.White);
            offsetPos = new Vector2(0f, 0f);
            source = new Rectangle(offset, Tile.tileSize - offset, Tile.tileSize - offset, offset);
            main.spriteBatch.Draw(texture, screen.DrawPos(main, pos + offsetPos), source, Color.White);
            offsetPos = new Vector2(Tile.tileSize - offset, offset);
            source = new Rectangle(0, 0, offset, Tile.tileSize - offset);
            main.spriteBatch.Draw(texture, screen.DrawPos(main, pos + offsetPos), source, Color.White);
            offsetPos = new Vector2(Tile.tileSize - offset, 0f);
            source = new Rectangle(0, Tile.tileSize - offset, offset, offset);
            main.spriteBatch.Draw(texture, screen.DrawPos(main, pos + offsetPos), source, Color.White);
        }

        public override void OnPlayerCollide(Room room, int x, int y, Player player)
        {
            const float offset = 0.25f;
            const float size = Tile.tileSize * (1 - 2 * offset);
            Hitbox lavaBox = new Hitbox(Tile.tileSize * new Vector2(x + offset, y + offset), size, size);
            Hitbox playerBox = player.hitbox;
            if (playerBox.topRight.X >= lavaBox.bottomLeft.X && playerBox.topLeft.X <= lavaBox.bottomRight.X
                && playerBox.bottomLeft.Y >= lavaBox.topLeft.Y && playerBox.topLeft.Y <= lavaBox.bottomLeft.Y)
            {
                player.TakeDamage(10);
            }
        }

        public static void LoadContent(ContentManager loader)
        {
            texture = loader.Load<Texture2D>("Lava");
        }
    }
}
