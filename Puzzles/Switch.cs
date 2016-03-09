using System;
using System.Collections.Generic;   // This is for lists. If you end up not having any lists, you won't need this
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Slip.Puzzles
{
    public class Switch : Puzzle
    {
        public static Texture2D texture;
        public const int size = 20;
        public Vector2 position;
        public bool on = false;  // This is False by default, as the switch is not turned on

        public override void Draw(GameScreen screen, int x, int y, Main main)
        {
            main.spriteBatch.Draw(texture, screen.DrawPos(main, position), null, Color.White, texture.Center()); 
        }

        public void ActiveSwitch(Player player)
        {
            if (Vector2.Distance(player.position, this.position) <= 20f)
            {
                this.on = true;
            }
        }
    }
}