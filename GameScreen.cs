using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Slip
{
    public class GameScreen : Screen
    {
        public const int tileSize = 40;

        public Player player;
        public int floorWidth;
        public int floorHeight;
        public byte[,] floor;

        public override void Initialize(Main main)
        {
            base.Initialize(main);
            player = new Player();
            floorWidth = 50;
            floorHeight = 50;
            floor = new byte[floorWidth, floorHeight];
            for (int x = 0; x < floorWidth; x++)
            {
                for (int y = 0; y < floorHeight; y++)
                {
                    floor[x, y] = 1;
                }
            }
        }

        public override void UpdateScreen(Main main)
        {
            player.Move();
        }

        public override void DrawScreen(Main main)
        {
            for (int x = 0; x < floorWidth; x++)
            {
                for (int y = 0; y < floorHeight; y++)
                {
                    Texture2D texture = null;
                    if (floor[x, y] == 1)
                    {
                        texture = floorTexture;
                    }
                    if (texture != null)
                    {
                        Vector2 worldPos = tileSize * new Vector2(x, y);
                        main.spriteBatch.Draw(floorTexture, DrawPos(main, worldPos), null, Color.White, Vector2.Zero);
                    }
                }
            }
            player.Draw(main);
        }

        public Vector2 DrawPos(Main main, Vector2 pos)
        {
            return size / 2f + pos - player.position;
        }

        private Texture2D floorTexture;

        public override void LoadContent(ContentManager loader)
        {
            base.LoadContent(loader);
            Player.LoadContent(loader);
            floorTexture = loader.Load<Texture2D>("Floor");
        }
    }
}
