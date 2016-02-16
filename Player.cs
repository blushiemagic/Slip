using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Slip
{
    public class Player
    {
        public static Texture2D texture;
        public const int size = 20;
        public Vector2 position;
        public bool dead;

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

        public Hitbox hitbox
        {
            get
            {
                return new Hitbox(position, size, size);
            }
        }

        public void Move(Room room)
        {
            Vector2 velocity = Vector2.Zero;
            if (Main.IsKeyPressed(Keys.Up))
            {
                velocity.Y -= 1f;
            }
            if (Main.IsKeyPressed(Keys.Down))
            {
                velocity.Y += 1f;
            }
            if (Main.IsKeyPressed(Keys.Left))
            {
                velocity.X -= 1f;
            }
            if (Main.IsKeyPressed(Keys.Right))
            {
                velocity.X += 1f;
            }
            if (velocity != Vector2.Zero)
            {
                velocity.Normalize();
                velocity *= 4f;
                TopLeft = Collision.MovePos(TopLeft, size, size, velocity, room);
            }
        }

        public void Draw(Main main)
        {
            main.spriteBatch.Draw(texture, main.Center(), null, Color.White, texture.Center());
        }

        public static void LoadContent(ContentManager loader)
        {
            texture = loader.Load<Texture2D>("Player");
        }
    }
}
