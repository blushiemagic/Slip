using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Slip.Bullets;

namespace Slip.Enemies
{
    public class FixedTurret : Enemy
    {
        public static Texture2D texture;
        public static Texture2D bulletTexture;
        private int shootTimer;
        public int maxShootTimer = 60;
        private Vector2 shootVelocity;

        public FixedTurret(Vector2 pos, Vector2 shoot) : base(pos, 20f)
        {
            this.shootTimer = 0;
            this.shootVelocity = shoot;
        }

        public FixedTurret(Vector2 pos, float direction, float speed = 4f) : base(pos, 20f)
        {
            this.shootTimer = 0;
            this.shootVelocity = speed * Helper.AngleToVector2(direction);
        }

        public override void Update(Room room, Player player)
        {
            shootTimer++;
            if (shootTimer >= maxShootTimer)
            {
                PositionalBullet bullet = new PositionalBullet(position, shootVelocity, 10f, bulletTexture, 120);
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
            texture = loader.Load<Texture2D>("Turret");
            bulletTexture = loader.Load<Texture2D>("Bullet");
        }
    }
}
