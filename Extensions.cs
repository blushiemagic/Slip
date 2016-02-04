using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Slip
{
    public static class Extensions
    {
        public static Vector2 Center(this Texture2D texture)
        {
            return new Vector2(texture.Width / 2, texture.Height / 2);
        }

        public static void Draw(this SpriteBatch spriteBatch, Texture2D texture, Vector2 position, Rectangle? sourceRectangle,
            Color color, Vector2 origin, bool flip = false)
        {
            spriteBatch.Draw(texture, position, sourceRectangle, color, 0f, origin, 1f,
                flip ? SpriteEffects.FlipHorizontally : SpriteEffects.None, 1f);
        }
    }
}
