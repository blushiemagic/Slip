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
        public float shootSpeed = 4f;
        public int fireTime = 120;
        private int attackType = 0;

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

		public EarthElemental(Vector2 pos) : base(pos, 20f)
        {
            shootTimer = 0;
        }

        public override void Update(Room room, Player player)
        {
            shootTimer++;
            if (shootTimer >= maxShootTimer)
            {
                if (attackType % 2 == 0)
                {

                    EarthPillar singlePillar = new EarthPillar(player.position, 20, PreEarthPillar, EarthPillar, 120);
                    room.bullets.Add(singlePillar);
                } else
                {
                    EarthPillar topPillar = new EarthPillar(player.position + new Vector2(0, -60), 20, PreEarthPillar, EarthPillar, 120);
                    EarthPillar bottomPillar = new EarthPillar(player.position + new Vector2(0, 60), 20, PreEarthPillar, EarthPillar, 120);

                    room.bullets.Add(topPillar);
                    room.bullets.Add(bottomPillar);
                }
                attackType++;
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