using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Slip.Enemies;
using Slip.Puzzles;

namespace Slip.Levels
{
    public class Tutorial : GameScreen
    {
        Room startRoom = new Room();
        Room room2 = new Room(30, 30);

        public override Room SetupLevel(Player player)
        {
            startRoom.FillFloor(0, startRoom.width - 1, 0, startRoom.height - 1);
            startRoom.FillFloor(20, 29, 20, 29, 2);
            startRoom.AddWallBorder(0, startRoom.width - 1, 0, startRoom.height - 1);
            for (int x = 22; x < 28; x++)
            {
                startRoom.tiles[x, 17].Wall = 1;
                startRoom.tiles[x, 32].Wall = 1;
            }
            for (int y = 22; y < 28; y++)
            {
                startRoom.tiles[17, y].Wall = 1;
                startRoom.tiles[32, y].Wall = 1;
            }
            startRoom.enemies.Add(new Turret(Room.TileToWorldPos(25, 25)));
            startRoom.AddPuzzle(1, 1, new Checkpoint());
            startRoom.AddPuzzle(startRoom.width - 2, startRoom.height - 2, new Checkpoint());
            startRoom.AddPuzzle(1, startRoom.height - 2, new Portal(room2, Tile.tileSize * new Vector2(1.5f, 1.5f)));

            room2.FillFloor(0, room2.width - 1, 0, room2.height - 1, 2);
            room2.AddWallBorder(0, room2.width - 1, 0, room2.height - 1);
            room2.AddPuzzle(1, 1, new Checkpoint());
            room2.AddPuzzle(room2.width - 2, room2.height - 2,
                new Portal(startRoom, Tile.tileSize * new Vector2(1.5f, 1.5f)));

            return startRoom;
        }

        public override void LoadContent(ContentManager loader)
        {
            base.LoadContent(loader);
            floorTexture[1] = loader.Load<Texture2D>("Floor1");
            floorTexture[2] = loader.Load<Texture2D>("Floor2");
            wallTexture[1] = loader.Load<Texture2D>("Wall1");
            Turret.LoadContent(loader);
        }
    }
}
