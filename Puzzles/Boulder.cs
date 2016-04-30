using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Slip.Bullets;

namespace Slip.Puzzles
{
    public class Boulder : Puzzle
    {
        public static Texture2D texture;

        public bool right;
        public bool bottom;

        public Boulder(bool right, bool bottom)
        {
            this.right = right;
            this.bottom = bottom;
        }

        public override void Draw(GameScreen screen, int x, int y, Main main)
        {
            Vector2 drawPos = screen.DrawPos(main, Room.TileToWorldPos(x, y));
            Rectangle frame = new Rectangle(0, 0, Tile.tileSize, Tile.tileSize);
            if (right)
            {
                frame.X = Tile.tileSize;
            }
            if (bottom)
            {
                frame.Y = Tile.tileSize;
            }
            main.spriteBatch.Draw(texture, drawPos, frame, Color.White, Vector2.Zero);
        }

        public override bool SolidCollision()
        {
            return true;
        }

        public static void Add(Room room, int x, int y)
        {
            room.AddPuzzle(x, y, new Boulder(false, false));
            room.AddPuzzle(x + 1, y, new Boulder(true, false));
            room.AddPuzzle(x, y + 1, new Boulder(false, true));
            room.AddPuzzle(x + 1, y + 1, new Boulder(true, true));
        }

        public static void Throw(Room room, int x, int y, Vector2 velocity)
        {
            if (((Boulder)room.tiles[x, y].puzzle).bottom)
            {
                y--;
            }
            if (((Boulder)room.tiles[x, y].puzzle).right)
            {
                x--;
            }
            room.RemovePuzzle(x, y);
            room.RemovePuzzle(x + 1, y);
            room.RemovePuzzle(x, y + 1);
            room.RemovePuzzle(x + 1, y + 1);
            room.bullets.Add(new ThrownBoulder(Room.TileToWorldPos(x + 1, y + 1), velocity));
        }

        public static void LoadContent(ContentManager loader)
        {
            texture = loader.Load<Texture2D>("Boulder");
        }
    }
}
