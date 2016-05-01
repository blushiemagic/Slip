using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Slip.Puzzles
{
    public class Door : Puzzle
    {
        public Texture2D closedTexture;
        public Texture2D openVerticalTexture;
        public Texture2D openHorizontalTexture;
        public bool open = false;
        private bool vertical;

        public Door(bool vertical, bool open = false)
        {
            this.vertical = vertical;
            this.open = open;
        }

        public override void Draw(GameScreen screen, int x, int y, Main main)
        {
            Texture2D texture = closedTexture;
            if (open)
            {
                texture = vertical ? openVerticalTexture : openHorizontalTexture;
            }
            main.spriteBatch.Draw(texture, screen.DrawPos(main, Room.TileToWorldPos(x, y)), null, Color.White);
        }

        public void Open()
        {
            open = true;
        }

        public void Close()
        {
            open = false;
        }

        public void Toggle()
        {
            open = !open;
        }

        public override bool SolidCollision()
        {
            return !open;
        }
    }
}