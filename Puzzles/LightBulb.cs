using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Slip.Puzzles
{
    public class LightBulb : Puzzle
    {
        public static Texture2D offTexture;
        public static Texture2D onTexture;

        public bool light;

        public LightBulb(bool light = false)
        {
            this.light = light;
        }

        public override void Draw(GameScreen screen, int x, int y, Main main)
        {
            Texture2D texture = light ? onTexture : offTexture;
            main.spriteBatch.Draw(texture, screen.DrawPos(main, Room.TileToWorldPos(x, y)), null, Color.White);
        }

        public override bool SolidCollision()
        {
            return true;
        }

        public void Switch()
        {
            light = !light;
        }

        public static void LoadContent(ContentManager loader)
        {
            offTexture = loader.Load<Texture2D>("LightBulbOff");
            onTexture = loader.Load<Texture2D>("LightBulbOn");
        }
    }
}
