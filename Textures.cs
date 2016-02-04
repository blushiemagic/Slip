using System;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Slip
{
    public static class Textures
    {
        public static Texture2D Panel;

        public static void LoadContent(ContentManager loader)
        {
            Panel = loader.Load<Texture2D>("Panel");
        }
    }
}
