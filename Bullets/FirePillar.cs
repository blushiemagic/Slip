using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Slip.Bullets
{
    public class FirePillar : Bullet
    {
        public static Texture2D warningTexture;
        public static Texture2D pillarTexture;

        public int x;
        public int y;
        public int timeLeft;
        public const int damageTime = 30;

        public FirePillar(int x, int y, int timeLeft)
        {
            this.x = x;
            this.y = y;
            this.timeLeft = timeLeft;
        }

        public override bool UpdateBullet(Room room, Player player)
        {
            timeLeft--;
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
