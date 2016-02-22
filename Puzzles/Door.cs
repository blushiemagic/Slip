using System;
using System.Collections.Generic;   // This is for lists. If you end up not having any lists, you won't need this
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Slip.Puzzles
{
    public class Door : Puzzle
    {
        public static Texture2D texture;
        public const int size = 20; // Size of the door. Change this if necessary, especially if we want doors of different sizes
        public Vector2 position;
        public bool open = false;  // This is False by default, as the door is closed by default

        public override void Draw(GameScreen screen, Main main)
        {
            main.spriteBatch.Draw(texture, screen.DrawPos(main, position), null, Color.White, texture.Center());
        }

        public void OpenDoor(Player player)
        {
            if (Vector2.Distance(player.position, this.position) <= 20f)
            {
                this.open = true;
            }
        }
    }
}