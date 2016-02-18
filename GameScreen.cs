using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Slip
{
    public abstract class GameScreen : Screen
    {
        public const int tileSize = 40;
        
        public Player player;
        public Room currentRoom;

        public override void Initialize(Main main)
        {
            base.Initialize(main);
            player = new Player();
            player.position = new Vector2(tileSize * 1.5f, tileSize * 1.5f);
            currentRoom = SetupLevel();
        }

        public abstract Room SetupLevel();

        public override void UpdateScreen(Main main)
        {
            foreach (Enemy enemy in currentRoom.enemies)
            {
                enemy.Update(currentRoom);
            }
            player.Update(currentRoom);
        }

        public void ChangeRoom(Room room, Vector2 position)
        {
            currentRoom = room;
            player.position = position;
            room.EnterRoom(player);
        }

        public override void DrawScreen(Main main)
        {
            int startTileX, endTileX, startTileY, endTileY;
            ScreenBoundTiles(out startTileX, out endTileX, out startTileY, out endTileY);
            for (int x = startTileX; x <= endTileX; x++)
            {
                for (int y = startTileY; y <= endTileY; y++)
                {
                    Texture2D texture = floorTexture[currentRoom.floor[x, y]];
                    if (texture != null)
                    {
                        Vector2 worldPos = tileSize * new Vector2(x, y);
                        main.spriteBatch.Draw(texture, DrawPos(main, worldPos), null, Color.White, Vector2.Zero);
                    }
                }
            }
            foreach (Enemy enemy in currentRoom.enemies)
            {
                enemy.Draw(this, main);
            }
            player.Draw(main);
            for (int x = startTileX; x <= endTileX; x++)
            {
                for (int y = startTileY; y <= endTileY; y++)
                {
                    Texture2D texture = wallTexture[currentRoom.wall[x, y]];
                    if (texture != null)
                    {
                        Vector2 worldPos = tileSize * new Vector2(x, y);
                        main.spriteBatch.Draw(texture, DrawPos(main, worldPos), null, Color.White, Vector2.Zero);
                    }
                }
            }
            DrawHUD(main);
        }

        private void DrawHUD(Main main)
        {
            string text = "LVL1  HP:" + player.life + "/" + player.maxLife + "  EXP:0/9999";
            main.spriteBatch.DrawBorderString(Textures.Font, text, new Vector2(10f, 10f), Color.White, Color.Black, 2);
        }

        public bool BoxOnScreen(Hitbox box)
        {
            if (box.topLeft.X > size.X / 2f + player.position.X)
            {
                return false;
            }
            if (box.topRight.X < -size.X / 2f + player.position.X)
            {
                return false;
            }
            if (box.topLeft.Y > size.Y / 2f + player.position.Y)
            {
                return false;
            }
            if (box.bottomLeft.Y * tileSize < -size.Y / 2f + player.position.Y)
            {
                return false;
            }
            return true;
        }

        public void ScreenBoundTiles(out int left, out int right, out int top, out int bottom)
        {
            left = (int)Math.Floor((-size.X / 2f + player.position.X) / tileSize);
            if (left < 0)
            {
                left = 0;
            }
            right = (int)Math.Floor((size.X / 2f + player.position.X) / tileSize);
            if (right >= currentRoom.floorWidth)
            {
                right = currentRoom.floorWidth - 1;
            }
            top = (int)Math.Floor((-size.Y / 2f + player.position.Y) / tileSize);
            if (top < 0)
            {
                top = 0;
            }
            bottom = (int)Math.Floor((size.Y / 2f + player.position.Y) / tileSize);
            if (bottom >= currentRoom.floorHeight)
            {
                bottom = currentRoom.floorHeight - 1;
            }
        }

        public Vector2 DrawPos(Main main, Vector2 pos)
        {
            return size / 2f + pos - player.position;
        }

        protected Texture2D[] floorTexture;
        protected Texture2D[] wallTexture;

        public override void LoadContent(ContentManager loader)
        {
            base.LoadContent(loader);
            Player.LoadContent(loader);
        }
    }
}
