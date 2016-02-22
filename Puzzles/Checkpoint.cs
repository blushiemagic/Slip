using System;
using Microsoft.Xna.Framework;

namespace Slip.Puzzles
{
    public class Checkpoint : Puzzle
    {
        public Room room;
        public Vector2 position;

        public override void Initialize(Room room, int x, int y)
        {
            this.room = room;
            this.position = Tile.tileSize * new Vector2(x + 0.5f, y + 0.5f);
        }

        public override void Update(Room room, int x, int y)
        {
            
        }
    }
}
