using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Slip
{
    public abstract class Enemy
    {
        public Vector2 position;
        public float size;
        public int life = 1;
        public bool invincible = false;
        public int hurtCool = 0;
        public bool temporary = false;

        public float radius
        {
            get
            {
                return size / 2f;
            }
        }

        public bool CanBeHit
        {
            get
            {
                return !invincible && hurtCool <= 0;
            }
        }

        public Enemy(Vector2 pos, float size)
        {
            this.position = pos;
            this.size = size;
        }

        public void UpdateEnemy(Room room, Player player)
        {
            if (hurtCool > 0)
            {
                hurtCool--;
            }
            Update(room, player);
        }

        public abstract void Update(Room room, Player player);

        public abstract void Draw(GameScreen screen, Main main);

        public bool Collides(Player player)
        {
            return Vector2.Distance(position, player.position) < radius + player.radius;
        }

        public bool TakeDamage(int damage)
        {
            if (CanBeHit)
            {
                life -= damage;
                return life <= 0;
            }
            return false;
        }
    }
}
