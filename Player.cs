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
        public Vector2 position;
        public bool dead;

        public void Move()
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
                position += velocity;
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
