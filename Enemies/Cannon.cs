using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Slip.Bullets;

namespace Slip.Enemies
{
    public class Cannon : Enemy
    {
        public static Texture2D texture;

        public Vector2 velocity;
        public int cooldown = 0;

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

        public Cannon(Vector2 pos, Vector2 velocity) : base(pos, 40f)
        {
            this.velocity = velocity;
        }

        public override void Update(Room room, Player player)
        {
            if (cooldown > 0)
            {
                cooldown--;
            }
            else
            {
                bool collided;
                TopLeft = Collision.MovePos(TopLeft, size, size, velocity, room, out collided);
                if (collided)
                {
                    velocity *= -1f;
                }
            }
        }

        public void Fire(Room room)
        {
            if (cooldown <= 0)
            {
                cooldown = 40;
                room.bullets.Add(new CannonBall(this, position, new Vector2(0f, -8f)));
            }
        }

        public override void Draw(GameScreen screen, Main main)
        {
            main.spriteBatch.Draw(texture, screen.DrawPos(main, position), null, Color.White, texture.Center());
        }

        public static void LoadContent(ContentManager loader)
        {
            texture = loader.Load<Texture2D>("Cannon");
            CannonBall.LoadContent(loader);
        }
    }
}
