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
            for (int k = 0; k < room.enemies.Count; k++)
            {
                Enemy enemy = room.enemies[k];
                if (Vector2.Distance(enemy.position, position) <= radius + enemy.radius)
                {
                    if (enemy.TakeDamage(1, room, player))
                    {
                        k--;
                    }
                }
            }
        }
    }
}
