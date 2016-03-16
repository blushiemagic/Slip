using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Slip.Puzzles
{
    public class Switch : Puzzle
    {
        public static Texture2D texture;
        public static Texture2D texturePressed;
        public const int size = 20;
        public bool pressed = false;
        public delegate void SwitchAction(Room room, Player player);
        private SwitchAction action;

        public Switch(SwitchAction action)
        {
            this.action = action;
        }

        public override void Draw(GameScreen screen, int x, int y, Main main)
        {
            Texture2D text = pressed ? texturePressed : texture;
            main.spriteBatch.Draw(text, screen.DrawPos(main, Room.TileToWorldPos(x, y) + new Vector2(10f)), null, Color.White); 
        }

        public override void OnPlayerCollide(Room room, int x, int y, Player player)
        {
            Vector2 pos = Tile.tileSize * new Vector2(x + 0.5f, y + 0.5f);
            if (!pressed && Vector2.Distance(pos, player.position) < (size + Player.size) / 2f)
            {
                pressed = true;
                action(room, player);
            }
        }

        public static void LoadContent(ContentManager loader)
        {
            texture = loader.Load<Texture2D>("Switch");
            texturePressed = loader.Load<Texture2D>("SwitchPressed");
        }
    }
}