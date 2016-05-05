using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Slip;

namespace Slip.Bullets
{
    public class EarthPillar : Bullet
    {
        public Vector2 position;
        public float size;
        public Texture2D before;
        public Texture2D after;
        public Color color;
        public int totalTime;
        public int timeLeft;
        private bool collision;

        public Vector2 TopLeft
        {
            get
            {
                return position - 0.5f * new Vector2(size, size);
            }
            set
            {
                position = value + 0.5f * new Vector2(size, size);
            }
        }

        public float radius
        {
            get
            {
                return size / 2f;
            }
        }

        public EarthPillar(Vector2 position, float size, Texture2D before, Texture2D after, int totalTime)
        {
            this.position = position;
            this.size = size;
            this.before = before;
            this.after = after;
            this.color = Color.White;
            this.totalTime = totalTime;
            this.timeLeft = totalTime;
            this.collision = false;
        }

        public override sealed bool UpdateBullet(Room room, Player player)
        {
            if (timeLeft < totalTime * 3 / 4)
            {
                this.collision = true;

            }
            timeLeft--;
            return timeLeft < 0;
        }

        public override void Draw(GameScreen screen, Main main)
        {
            if (!collision)
            {
                main.spriteBatch.Draw(before, screen.DrawPos(main, position), null, color, before.Center());
            }
            else
            {
                main.spriteBatch.Draw(after, screen.DrawPos(main, position), null, color, after.Center());
            }
            
        }

        public override bool Collides(Player player)
        {
            if (!collision)
            {
                return false;
            }
            return Vector2.Distance(player.position, position) < player.radius + radius;
        }
    }
}