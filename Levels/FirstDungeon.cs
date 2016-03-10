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
        Room room1 = new Room(18, 7);
        Room room2 = new Room(18, 18);
        Room room3 = new Room(21, 21);
        Room room4 = new Room(5, 5);

        public override Room SetupLevel(Player player)
        {
            // Room 1 contents
            room1.FillFloor(0, room1.width - 1, 0, room1.height - 1);
            room1.AddWallBorder(0, room1.width - 1, 0, room1.height - 1);

            room1.AddPuzzle(4, 3, new Door(false));
            room1.tiles[4, 1].Wall = 1;
            room1.tiles[4, 2].Wall = 1;
            room1.tiles[4, 4].Wall = 1;
            room1.tiles[4, 5].Wall = 1;

            room1.enemies.Add(new Turret(Room.TileToWorldPos(17, 3)));
            room1.enemies.Add(new Turret(Room.TileToWorldPos(17, 4)));
            room1.enemies.Add(new Turret(Room.TileToWorldPos(17, 5)));
            room1.enemies.Add(new Turret(Room.TileToWorldPos(17, 2)));

            room1.AddPuzzle(15, 3, new Portal(room2, Tile.tileSize * new Vector2(1.5f, 1.5f)));
            room1.AddPuzzle(1, 1, new Checkpoint());

            // Room 2 contents
            // Ideally, we would want switches in this room to activate the portal to the next room. 
            room2.FillFloor(0, room2.width - 1, 0, room2.height - 1);
            room2.AddWallBorder(0, room2.width - 1, 0, room2.height - 1);
            
            for (int i = 8; i < 11; i++) {
                room2.enemies.Add(new Turret(Room.TileToWorldPos(i, 7)));
                room2.enemies.Add(new Turret(Room.TileToWorldPos(i, 11)));
                room2.enemies.Add(new Turret(Room.TileToWorldPos(7, i)));
                room2.enemies.Add(new Turret(Room.TileToWorldPos(11, i)));
            }

            room2.AddPuzzle(room2.width - 2, room2.height - 2, new Portal(room3, Tile.tileSize * new Vector2(10.5f, 10.5f)));
            room2.AddPuzzle(1, 1, new Checkpoint());

            // Room 3 contents
            // The room will have some enemies to fight. Ideally, when all the enemies are dead, the portal will open up. 
            room3.FillFloor(0, room3.width - 1, 0, room3.height - 1);
            room3.AddWallBorder(0, room3.width - 1, 0, room3.height - 1);

            room3.AddPuzzle(room3.width - 2, room3.height - 2, new Portal(room4, Tile.tileSize * new Vector2(1.5f, 1.5f)));
            room3.AddPuzzle(10, 10, new Checkpoint());

            // Room 4 contents
            room4.FillFloor(0, room4.width - 1, 0, room4.height - 1);
            room4.AddWallBorder(0, room4.width - 1, 0, room4.height - 1);


            // Returns the first room to be loaded
            return room1;
        }

        public override void LoadContent(ContentManager loader)
        {
            base.LoadContent(loader);
            LoadTileset("FirstDungeon", 1, 1, loader);
            Turret.LoadContent(loader);
            BrownDoor.LoadContent(loader);
        }
    }
}
