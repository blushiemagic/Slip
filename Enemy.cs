using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Slip
{
    public abstract class Enemy
    {
        public Vector2 position;
        public Vector2? defaultPosition = null;
        public float size;
        public bool boss;
        public int life = 1;
        public bool invincible = false;
        public int hurtCool = 0;
        public bool temporary = false;
        public delegate void DeathAction(Enemy enemy, Room room, Player player);
        public event DeathAction OnDeath;

        public float radius
        {
            get
            {
                return size / 2f;
            }
        }

        public bool IsHurt
        {
            get
            {
                return hurtCool > 0;
            }
        }

        public bool CanBeHit
        {
            get
            {
                return !invincible && !IsHurt;
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

        public bool TakeDamage(int damage, Room room, Player player)
        {
            if (CanBeHit)
            {
                life -= damage;
                if (life <= 0)
                {
                    Kill(room, player);
                    return true;
                }
                if (boss)
                {
                    hurtCool = 180;
                    room.bullets.Clear();
                }
                else
                {
                    hurtCool = 60;
                }
            }
            return false;
        }

        public void Kill(Room room, Player player)
        {
            if (OnDeath != null)
            {
                OnDeath(this, room, player);
            }
            room.enemies.Remove(this);
        }
    }
}
