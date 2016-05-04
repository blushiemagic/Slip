using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Slip.Bullets;

namespace Slip.Enemies
{
    public class EarthElemental : Enemy
    {

        public static Texture2D texture;
        public static Texture2D EarthPillar;
        public static Texture2D PreEarthPillar;
        private int shootTimer;
        public int maxShootTimer = 120;
        private Random rnd = new Random();

        public Vector2 TopLeft
        {
            get
            {
                return position - 0.5f * new Vector2(size, size);
            }
            set
            {
                position = value + 0.5f * new Vector2(size, size);
            }
        }

		public EarthElemental(Vector2 pos, int timerStart) : base(pos, 20f)
        {
            shootTimer = timerStart;
        }

        public override void Update(Room room, Player player)
        {
            shootTimer++;
            if (shootTimer >= maxShootTimer)
            {
                int centerPillarArbiter = rnd.Next() % 2;
                int topPillarArbiter = rnd.Next() % 2;
                int bottomPillarArbiter = rnd.Next() % 2;
                int rightPillarArbiter = rnd.Next() % 2;
                int leftPillarArbiter = rnd.Next() % 2;

                if (centerPillarArbiter + topPillarArbiter + bottomPillarArbiter + rightPillarArbiter + leftPillarArbiter == 5)
                {
                    centerPillarArbiter = 0;
                }

                if (centerPillarArbiter == 0)
                {
                    room.bullets.Add(new EarthPillar(player.position, 20, PreEarthPillar, EarthPillar, maxShootTimer));
                }
                if (topPillarArbiter == 0)
                {
                    room.bullets.Add(new EarthPillar(player.position + new Vector2(0, -40), 20, PreEarthPillar, EarthPillar, maxShootTimer));
                }
                if (bottomPillarArbiter == 0)
                {
                    room.bullets.Add(new EarthPillar(player.position + new Vector2(0, 40), 20, PreEarthPillar, EarthPillar, maxShootTimer));
                }
                if (rightPillarArbiter == 0)
                {
                    room.bullets.Add(new EarthPillar(player.position + new Vector2(40, 0), 20, PreEarthPillar, EarthPillar, maxShootTimer));
                }
                if (leftPillarArbiter == 0)
                {
                    room.bullets.Add(new EarthPillar(player.position + new Vector2(-40, 0), 20, PreEarthPillar, EarthPillar, maxShootTimer));
                }
                shootTimer = 0;
            }
        }



        public override void Draw(GameScreen screen, Main main)
        {
            main.spriteBatch.Draw(texture, screen.DrawPos(main, position), null, Color.White, texture.Center());
        }

        public static void LoadContent(ContentManager loader)
        {
            texture = loader.Load<Texture2D>("EarthElemental");
            EarthPillar = loader.Load<Texture2D>("EarthPillar");
            PreEarthPillar = loader.Load<Texture2D>("WarningX");
        }
    }
}