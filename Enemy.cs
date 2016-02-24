using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Slip
{
    public abstract class Enemy
    {
        public Vector2 position;
        public float size;

        public float radius
        {
            get
            {
                return size / 2f;
            }
        }

        public Enemy(Vector2 pos, float size)
        {
            this.position = pos;
            this.size = size;
        }

        public abstract void Update(Room room, Player player);

        public abstract void Draw(GameScreen screen, Main main);

        public bool Collides(Player player)
        {
            return Vector2.Distance(position, player.position) < radius + player.radius;
        }
    }
}
