using System;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Slip
{
    public static class Textures
    {
        public static SpriteFont Font;
        public static Texture2D Panel;
        public static Texture2D Pixel;

        public static void LoadContent(ContentManager loader)
        {
            Font = loader.Load<SpriteFont>("Font");
            Panel = loader.Load<Texture2D>("Panel");
            Pixel = loader.Load<Texture2D>("Pixel");
        }
    }
}
