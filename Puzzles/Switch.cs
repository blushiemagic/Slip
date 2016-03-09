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
        private Vector2 cameraTarget;
        private CameraEvent.ActionEvent action;
        private int actionLength;

        public Switch(Vector2 target, CameraEvent.ActionEvent action, int length = 0)
        {
            this.cameraTarget = target;
            this.action = action;
            this.actionLength = length;
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
                room.cameraEvent = new CameraEvent(cameraTarget, action, actionLength);
            }
        }

        public static void LoadContent(ContentManager loader)
        {
            texture = loader.Load<Texture2D>("Switch");
            texturePressed = loader.Load<Texture2D>("SwitchPressed");
        }
    }
}