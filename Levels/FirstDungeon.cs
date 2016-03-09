using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Slip.Enemies;
using Slip.Puzzles;

namespace Slip.Levels
{
    public class FirstDungeon : GameScreen
    {
        Room room1 = new Room(18, 14);
        //Room room2 = new Room(5, 5);

        public override Room SetupLevel(Player player)
        {
            room1.FillFloor(0, room1.width - 1, 0, room1.height - 1);
            room1.AddWallBorder(0, room1.width - 1, 0, room1.height - 1);
            
            
            
            //room1.AddPuzzle(room1.width - 2, room1.height - 2, new Checkpoint());
			// startRoom.AddPuzzle(1, startRoom.height - 2, new Portal(room2, Tile.tileSize * new Vector2(1.5f, 1.5f)));

            for (int x = 1; x < 13; x++)
            {
                room1.tiles[x, 5].Wall = 1;
                room1.tiles[x, 8].Wall = 1;
            }
            for (int x = 1; x < 12; x++)
            {
                room1.tiles[x, 6].Wall = 1;
                room1.tiles[x, 7].Wall = 1;
            }
            room1.enemies.Add(new Turret(Room.TileToWorldPos(12, 7)));
            room1.AddPuzzle(1, 1, new Checkpoint());


                // room2.AddPuzzle(room2.width - 2, room2.height - 2,
                //new Portal(startRoom, Tile.tileSize * new Vector2(1.5f, 1.5f)));

                return room1;
        }

        public override void LoadContent(ContentManager loader)
        {
            base.LoadContent(loader);
            LoadTileset("FirstDungeon", 1, 1, loader);
            Turret.LoadContent(loader);
        }
    }
}
