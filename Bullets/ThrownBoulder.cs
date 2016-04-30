using System;
using Microsoft.Xna.Framework;
using Slip.Puzzles;

namespace Slip.Bullets
{
    public class ThrownBoulder : PositionalBullet
    {
        public ThrownBoulder(Vector2 position, Vector2 velocity)
            : base(position, velocity, 80f, Boulder.texture, 600)
        {
            
        }

        public override void PostUpdate(Room room, Player player)
        {
            foreach (Enemy enemy in room.enemies)
            {
                if (Vector2.Distance(enemy.position, position) <= radius + enemy.radius)
                {
                    enemy.TakeDamage(1, room, player);
                }
            }
        }
    }
}
