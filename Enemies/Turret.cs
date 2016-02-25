using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Slip.Bullets;

namespace Slip.Enemies
{
    // This is the general class for turrets. All other turrets are descendants of this class. 
    public class Turret : Enemy
    {
        public static Texture2D texture;
        public static Texture2D bulletTexture;
        private int shootTimer;

        public Turret(Vector2 pos) : base(pos, 20f)
        {
            shootTimer = 0;
        }

        public override void Update(Room room, Player player)
        {
            Vector2 offset = player.position - position;
            Vector2 range = Main.instance.Size();
            if (Math.Abs(offset.X) > range.X / 2f || Math.Abs(offset.Y) > range.Y / 2f)
            {
                shootTimer = 0;
            }
            else
            {
                shootTimer++;
                if (shootTimer >= 60)
                {
                    Vector2 direction = offset;
                    if (direction != Vector2.Zero)
                    {
                        direction.Normalize();
                    }
                    direction *= 4f;
                    PositionalBullet bullet = new PositionalBullet(position, direction, 10f, bulletTexture, 120);
                    room.bullets.Add(bullet);
                    shootTimer = 0;
                }
            }
        }

        public override void Draw(GameScreen screen, Main main)
        {
            main.spriteBatch.Draw(texture, screen.DrawPos(main, position), null, Color.White, texture.Center());
        }

        public static void LoadContent(ContentManager loader)
        {
            texture = loader.Load<Texture2D>("Turret");
            bulletTexture = loader.Load<Texture2D>("Bullet");
        }
    }
}