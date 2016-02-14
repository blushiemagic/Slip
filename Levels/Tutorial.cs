using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Slip.Levels
{
    public class Tutorial : GameScreen
    {
        Room startRoom;

        public override Room SetupLevel()
        {
            startRoom = new Room();
            for (int x = 0; x < startRoom.floorWidth; x++)
            {
                for (int y = 0; y < startRoom.floorHeight; y++)
                {
                    startRoom.floor[x, y] = 1;
                }
            }
            for (int x = 20; x < 30; x++)
            {
                for (int y = 20; y < 30; y++)
                {
                    startRoom.floor[x, y] = 2;
                }
            }
            for (int x = 22; x < 28; x++)
            {
                startRoom.wall[x, 17] = 1;
                startRoom.wall[x, 32] = 1;
            }
            for (int y = 22; y < 28; y++)
            {
                startRoom.wall[17, y] = 1;
                startRoom.wall[32, y] = 1;
            }
            return startRoom;
        }

        public override void LoadContent(ContentManager loader)
        {
            base.LoadContent(loader);
            floorTexture = new Texture2D[3];
            floorTexture[1] = loader.Load<Texture2D>("Floor1");
            floorTexture[2] = loader.Load<Texture2D>("Floor2");
            wallTexture = new Texture2D[2];
            wallTexture[1] = loader.Load<Texture2D>("Wall1");
        }
    }
}
