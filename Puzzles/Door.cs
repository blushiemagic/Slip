using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Slip.Puzzles
{
    public class Door : Puzzle
    {
        public static Texture2D closedTexture;
        public static Texture2D openVerticalTexture;
        public static Texture2D openHorizontalTexture;
        protected bool open = false;
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

        public override bool SolidCollision()
        {
            return !open;
        }

        public override bool PlayerInteraction(Room room, int x, int y, Player player)
        {
            if (!open)
            {
                open = true;
                return true;
            }
            return false;
        }

        public static void LoadContent(ContentManager loader)
        {
            closedTexture = loader.Load<Texture2D>("DoorClosed");
            openVerticalTexture = loader.Load<Texture2D>("DoorOpenVertical");
            openHorizontalTexture = loader.Load<Texture2D>("DoorOpenHorizontal");
        }
    }
}