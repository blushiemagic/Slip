using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Slip.Bullets;

namespace Slip.Enemies
{
    public class FireCaster : Enemy
    {
        public static Texture2D texture;
        public static Texture2D fireTexture;
        private int shootTimer;
        public int maxShootTimer = 60;
        public float shootSpeed = 4f;
        public int fireTime = 120;

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

        public FireCaster(Vector2 pos) : base(pos, 20f)
        {
            shootTimer = 0;
        }

        public override void Update(Room room, Player player)
        {
            Vector2 offset = player.position - position;
            float distance = offset.Length();
            float range = Tile.tileSize * 6f;
            float speed = 2f;
            if (Math.Abs(distance - range) < speed)
            {
                speed = Math.Abs(distance - range);
            }
            if (distance != 0f)
            {
                offset *= speed / distance;
            }
            if (distance < range)
            {
                offset *= -1f;
            }
            bool collided;
            TopLeft = Collision.MovePos(TopLeft, size, size, offset, room, out collided);

            offset = player.position - position;
            shootTimer++;
            if (shootTimer >= maxShootTimer)
            {
                Vector2 direction = offset;
                if (direction != Vector2.Zero)
                {
                    direction.Normalize();
                }
                direction *= shootSpeed;
                PositionalBullet bullet = new PositionalBullet(position, direction, 10f, fireTexture, fireTime);
                room.bullets.Add(bullet);
                shootTimer = 0;
            }
        }

        public override void Draw(GameScreen screen, Main main)
        {
            main.spriteBatch.Draw(texture, screen.DrawPos(main, position), null, Color.White, texture.Center());
        }

        public static void LoadContent(ContentManager loader)
        {
            texture = loader.Load<Texture2D>("FireCaster");
            fireTexture = loader.Load<Texture2D>("Fire");
        }
    }
}
