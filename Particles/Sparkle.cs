using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Slip.Particles
{
    public class Sparkle : Particle
    {
        public static Texture2D texture;

        private Vector2 position;
        private Color color;
        private float rotation;
        private float rotateSpeed;
        private float scale;

        public Sparkle(Vector2 position, Color color)
        {
            this.position = position;
            this.color = color;
            this.rotation = (float)(Main.rand.NextDouble() * 2.0 * Math.PI);
            this.rotateSpeed = (float)(Main.rand.NextDouble() * 2.0 * Math.PI - Math.PI) / 60f;
            this.scale = 0.7f;
        }

        public Sparkle(Vector2 position) : this(position, Helper.FromHSB((float)Main.rand.NextDouble(), 0.9f, 1f)) { }

        public override bool Update(Room room, Player player)
        {
            rotation += rotateSpeed;
            scale *= 0.98f;
            return scale < 0.35f;
        }

        public override void Draw(GameScreen screen, Main main)
        {
            main.spriteBatch.Draw(texture, screen.DrawPos(main, position), null, color, rotation, texture.Center(),
                scale, SpriteEffects.None, 0f);
        }

        public static void LoadContent(ContentManager loader)
        {
            texture = loader.Load<Texture2D>("Sparkle");
        }
    }
}
