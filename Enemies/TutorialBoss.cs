using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Slip.Bullets;

namespace Slip.Enemies
{
    public class TutorialBoss : Enemy
    {
        public static Texture2D texture;
        public static Texture2D bulletTexture;
        private int timer = 0;
        private Vector2 center;
        private float angle;
        private float originalAngle;
        private float pathRadius;

        public TutorialBoss(Vector2 pos, Vector2 c) : base(pos, 40f)
        {
            boss = true;
            life = 3;
            center = c;
            angle = Helper.Vector2ToAngle(pos - c);
            originalAngle = angle;
            pathRadius = Vector2.Distance(pos, c);
        }

        public override void Update(Room room, Player player)
        {
            timer++;
            if (life == 2)
            {
                angle -= 4f / pathRadius;
            }
            else if (life == 1)
            {
                float distance = Vector2.Distance(position, player.position);
                float speed = 4f + 4f * (radius + player.radius) / distance;
                angle += speed / pathRadius;
            }
            position = center + pathRadius * Helper.AngleToVector2(angle);
            if (!IsHurt && timer >= 90)
            {
                timer = 0;
                float rotation = Helper.Vector2ToAngle(player.position - position);
                RingBullet ring;
                if (life == 3)
                {
                    ring = new RingBullet(position, 0f, rotation, 6, 10f, bulletTexture, 180);
                }
                else
                {
                    ring = new HomingRingBullet(position, 0f, rotation, 6, 10f, bulletTexture, 180,
                        life == 2 ? 1f : 2f);
                }
                ring.radiusSpeed = 4f;
                room.bullets.Add(ring);
            }
        }

        public void Reset()
        {
            life = 3;
            angle = originalAngle;
            position = center + pathRadius * Helper.AngleToVector2(angle);
            hurtCool = 0;
            timer = 0;
        }

        public override void Draw(GameScreen screen, Main main)
        {
            Color color = Color.White;
            if (IsHurt)
            {
                color *= 0.75f;
            }
            main.spriteBatch.Draw(texture, screen.DrawPos(main, position), null, color, texture.Center());
        }

        public static void LoadContent(ContentManager loader)
        {
            texture = loader.Load<Texture2D>("TutorialBoss");
            bulletTexture = loader.Load<Texture2D>("Bullet");
        }
    }
}
