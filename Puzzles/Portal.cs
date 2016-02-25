using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Slip.Particles;

namespace Slip.Puzzles
{
    public class Portal : Puzzle
    {
        public Room targetRoom;
        public Vector2 targetPos;

        public Portal(Room room, Vector2 position)
        {
            this.targetRoom = room;
            this.targetPos = position;
        }

        public override void Update(Room room, int x, int y, Player player)
        {
            Vector2 pos = new Vector2(x + (float)Main.rand.NextDouble(), y + (float)Main.rand.NextDouble());
            room.particles.Add(new Sparkle(pos * Tile.tileSize, Color.Aquamarine));
        }

        public override void OnPlayerCollide(Room room, int x, int y, Player player)
        {
            room.usePortal = this;
        }

        public static void LoadContent(ContentManager loader)
        {
            Sparkle.LoadContent(loader);
        }
    }
}
