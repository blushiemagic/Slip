using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Slip;

namespace Slip.Bullets
{
    public class PositionalBullet : Bullet
    {
        public Vector2 position;
        public Vector2 velocity;
        public float size;
        public bool wallCollide;
        public Texture2D texture;
        public Color color;
        public int timeLeft;

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

        public PositionalBullet(Vector2 position, Vector2 velocity, float size, Texture2D texture, int timeLeft)
        {
            this.position = position;
            this.velocity = velocity;
            this.size = size;
            this.wallCollide = true;
            this.texture = texture;
            this.color = Color.White;
            this.timeLeft = timeLeft;
        }

        public PositionalBullet(Vector2 position, float size, Texture2D texture, int timeLeft) :
            this(position, Vector2.Zero, size, texture, timeLeft) { }

        public override sealed bool UpdateBullet(Room room, Player player)
        {
            Vector2 oldPos = position;
            UpdateVelocity(room, player);
            if (wallCollide)
            {
                bool collided;
                TopLeft = Collision.MovePos(TopLeft, size, size, velocity, room, out collided);
                if (collided)
                {
                    return true;
                }
            }
            else
            {
                position += velocity;
            }
            PostUpdate(room, player);
            timeLeft--;
            return timeLeft < 0;
        }

        public virtual void UpdateVelocity(Room room, Player player) { }

        public virtual void PostUpdate(Room room, Player player) { }

        public override void Draw(GameScreen screen, Main main)
        {
            main.spriteBatch.Draw(texture, screen.DrawPos(main, position), null, color, texture.Center());
        }

        public override bool Collides(Player player)
        {
            return Vector2.Distance(player.position, position) < player.radius + radius;
        }
    }
}
