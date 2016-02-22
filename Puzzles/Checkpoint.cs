using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Slip.Particles;

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

        public override void Update(Room room, int x, int y, Player player)
        {
            if (player.lastCheckpoint != this)
            {
                Vector2 offset = new Vector2((float)Main.rand.NextDouble() * 40f - 20f,
                    (float)Main.rand.NextDouble() * 40f - 20f);
                room.particles.AddParticle(new Sparkle(position + offset));
            }
        }

        public override void OnPlayerCollide(Room room, int x, int y, Player player)
        {
            player.lastCheckpoint = this;
        }

        public static void LoadContent(ContentManager loader)
        {
            Sparkle.LoadContent(loader);
        }
    }
}
