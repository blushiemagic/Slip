using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Slip.Puzzles;

namespace Slip.Bullets
{
    public class FirePillar : Bullet
    {
        public static Texture2D warningTexture;
        public static Texture2D pillarTexture;

        public int x;
        public int y;
        public int timeLeft;
        public bool lava;
        public const int damageTime = 30;

        public FirePillar(int x, int y, int timeLeft, bool lava = false)
        {
            this.x = x;
            this.y = y;
            this.timeLeft = timeLeft;
            this.lava = lava;
        }

        public override bool UpdateBullet(Room room, Player player)
        {
            timeLeft--;
            if (timeLeft == 0 && lava && room.tiles[x, y].puzzle == null)
            {
                room.AddPuzzle(x, y, new Lava());
            }
            return timeLeft <= -damageTime;
        }

        public override void Draw(GameScreen screen, Main main)
        {
            Texture2D texture = timeLeft > 0 ? warningTexture : pillarTexture;
            Vector2 drawPos = screen.DrawPos(main, Room.TileToWorldPos(x, y));
            main.spriteBatch.Draw(texture, drawPos, Color.White);
        }

        public override bool Collides(Player player)
        {
            if (timeLeft > 0)
            {
                return false;
            }
            int left = x * Tile.tileSize;
            int right = left + Tile.tileSize;
            int top = y * Tile.tileSize;
            int bottom = top + Tile.tileSize;
            Vector2 playerPos = player.TopLeft;
            return playerPos.X + Player.size > left && playerPos.X < right
                && playerPos.Y + Player.size > top && playerPos.Y < bottom;
        }

        public static void LoadContent(ContentManager loader)
        {
            warningTexture = loader.Load<Texture2D>("FirePillarWarning");
            pillarTexture = loader.Load<Texture2D>("FirePillar");
        }
    }
}
