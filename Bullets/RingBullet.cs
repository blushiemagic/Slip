using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Slip.Bullets
{
    public class RingBullet : Bullet
    {
        public Vector2 center;
        public Vector2 velocity;
        public float radius;
        public float radiusSpeed;
        public float rotation;
        public float rotationSpeed;
        public readonly int numBullets;
        public float bulletSize;
        public Texture2D texture;
        public Color color;
        public int timeLeft;

        public float bulletRadius
        {
            get
            {
                return bulletSize / 2f;
            }
        }

        public RingBullet(Vector2 center, float radius, float rotation, int numBullets,
            float bulletSize, Texture2D texture, int timeLeft)
        {
            this.center = center;
            this.velocity = Vector2.Zero;
            this.radius = radius;
            this.radiusSpeed = 0f;
            this.rotation = rotation;
            this.rotationSpeed = 0f;
            this.numBullets = numBullets;
            this.bulletSize = bulletSize;
            this.texture = texture;
            this.color = Color.White;
            this.timeLeft = timeLeft;
        }

        public override sealed bool UpdateBullet(Room room, Player player)
        {
            UpdateState(room, player);
            center += velocity;
            radius += radiusSpeed;
            rotation += rotationSpeed;
            timeLeft--;
            return timeLeft < 0;
        }

        public virtual void UpdateState(Room room, Player player) { }

        public override void Draw(GameScreen screen, Main main)
        {
            for (int k = 0; k < numBullets; k++)
            {
                float angle = rotation + 2f * k / numBullets * (float)Math.PI;
                Vector2 pos = center + radius * Helper.AngleToVector2(angle);
                main.spriteBatch.Draw(texture, screen.DrawPos(main, pos), null, color, texture.Center());
            }
        }

        public override bool Collides(Player player)
        {
            for (int k = 0; k < numBullets; k++)
            {
                float angle = rotation + 2f * k / numBullets * (float)Math.PI;
                Vector2 pos = center + radius * Helper.AngleToVector2(angle);
                if (Vector2.Distance(player.position, pos) < player.radius + bulletRadius)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
