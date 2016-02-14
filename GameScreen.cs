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
            player.position = new Vector2(tileSize / 2f, tileSize / 2f);
            currentRoom = SetupLevel();
        }

        public abstract Room SetupLevel();

        public override void UpdateScreen(Main main)
        {
            player.Move();
        }

        public void ChangeRoom(Room room, Vector2 position)
        {
            currentRoom = room;
            player.position = position;
            room.EnterRoom(player);
        }

        public override void DrawScreen(Main main)
        {
            for (int x = 0; x < currentRoom.floorWidth; x++)
            {
                for (int y = 0; y < currentRoom.floorHeight; y++)
                {
                    Texture2D texture = floorTexture[currentRoom.floor[x, y]];
                    if (texture != null)
                    {
                        Vector2 worldPos = tileSize * new Vector2(x, y);
                        main.spriteBatch.Draw(texture, DrawPos(main, worldPos), null, Color.White, Vector2.Zero);
                    }
                }
            }
            player.Draw(main);
            for (int x = 0; x < currentRoom.floorWidth; x++)
            {
                for (int y = 0; y < currentRoom.floorHeight; y++)
                {
                    Texture2D texture = wallTexture[currentRoom.wall[x, y]];
                    if (texture != null)
                    {
                        Vector2 worldPos = tileSize * new Vector2(x, y);
                        main.spriteBatch.Draw(texture, DrawPos(main, worldPos), null, Color.White, Vector2.Zero);
                    }
                }
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
