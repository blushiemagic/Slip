using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Slip.Bullets;

namespace Slip.Enemies
{
    public class FireBoss : Enemy
    {
        public static Texture2D texture;
        public static Texture2D fireTexture;

        private int phase;
        private int timer;

        public FireBoss(Vector2 position) : base(position, 40f)
        {
            boss = true;
            life = 3;
            phase = 0;
            timer = 0;
        }

        public override void Update(Room room, Player player)
        {
            if (IsHurt)
            {
                phase = 0;
                timer = 0;
                return;
            }
            if (phase == 0)
            {
                WallAttack(room, player);
            }
        }

        private void WallAttack(Room room, Player player)
        {
            int warningTime = 30 * life;
            if (life == 1)
            {
                warningTime = 45;
            }
            int pillarTime = warningTime + FirePillar.damageTime;
            int waitTime = pillarTime + warningTime - 30;
            if (life == 1)
            {
                waitTime -= 15;
            }
            int playerX = (int)(player.position.X / Tile.tileSize);
            int playerY = (int)(player.position.Y / Tile.tileSize);
            if (timer == waitTime)
            {
                CreatePattern(room, (x, y) => x == playerX, warningTime);
            }
            else if (timer == 2 * waitTime)
            {
                CreatePattern(room, (x, y) => y == playerY, warningTime);
            }
            else if (timer == 3 * waitTime)
            {
                CreatePattern(room, (x, y) => Math.Abs(x - playerX) == Math.Abs(y - playerY), warningTime);
            }
            else if (timer == 4 * waitTime)
            {
                CreatePattern(room, (x, y) => x == playerX || y == playerY, warningTime);
            }
            else if (timer == 5 * waitTime)
            {
                CreatePattern(room, (x, y) => Math.Abs(x - playerX) <= 1, warningTime);
            }
            else if (timer == 6 * waitTime)
            {
                CreatePattern(room, (x, y) => Math.Abs(y - playerY) <= 1, warningTime);
            }
            else if (timer == 7 * waitTime)
            {
                CreatePattern(room, (x, y) => Math.Abs(Math.Abs(x - playerX) - Math.Abs(y - playerY)) <= 1, warningTime);
            }
            else if (timer == 8 * waitTime)
            {
                CreatePattern(room, (x, y) => Math.Abs(x - playerX) <= 1 || Math.Abs(y - playerY) <= 1, warningTime);
            }
            else if (timer == 9 * waitTime || timer == 11 * waitTime)
            {
                CreatePattern(room, (x, y) => Math.Abs(x - playerX) % 2 == Math.Abs(y - playerY) % 2, warningTime);
            }
            else if (timer == 10 * waitTime || timer == 12 * waitTime)
            {
                CreatePattern(room, (x, y) => x % 2 == playerX % 2 || y % 2 == playerY % 2, warningTime);
            }
            timer++;
            if (timer >= 13 * waitTime)
            {
                phase++;
                timer = 0;
            }
        }

        private void CreatePattern(Room room, Func<int, int, bool> filter, int timeLeft)
        {
            for (int x = 1; x < room.width - 1; x++)
            {
                for (int y = 1; y < room.height - 1; y++)
                {
                    if (filter(x, y))
                    {
                        room.bullets.Add(new FirePillar(x, y, timeLeft));
                    }
                }
            }
        }

        public void Reset()
        {
            life = 3;
            hurtCool = 0;
            phase = 0;
            timer = 0;
        }

        public override void Draw(GameScreen screen, Main main)
        {
            Color color = Color.White;
            if (IsHurt)
            {
                color *= 0.75f;
            }
            main.spriteBatch.Draw(texture, screen.DrawPos(main, position), null, color, texture.Center());
        }

        public static void LoadContent(ContentManager loader)
        {
            texture = loader.Load<Texture2D>("FireBoss");
            fireTexture = loader.Load<Texture2D>("Fire");
            FirePillar.LoadContent(loader);
        }
    }
}
