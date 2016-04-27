using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Slip.Enemies;
using Slip.Puzzles;

namespace Slip.Levels
{
    public class EarthDungeon : GameScreen
    {
        Room room1 = new Room(18, 7);
        Room room2 = new Room(20, 20);
        Room room3 = new Room(30, 5);
        Room room4 = new Room(5, 5);

        public override Room SetupLevel(Player player)
        {
            // Room 1 contents
            room1.SetupFloorsAndWalls();
            room1.AddPuzzle(4, 3, new BrownDoor(false));
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
            room2.SetupFloorsAndWalls();
            room2.tiles[room2.width - 2, room2.height - 3].Wall = 1;
            room2.tiles[room2.width - 3, room2.height - 3].Wall = 1;

            room2.enemies.Add(new Turret(Room.TileToWorldPos(10, 12)));
            room2.enemies.Add(new Turret(Room.TileToWorldPos(11, 11)));
            room2.enemies.Add(new Turret(Room.TileToWorldPos(12, 10)));
            room2.enemies.Add(new Turret(Room.TileToWorldPos(11, 9)));
            room2.enemies.Add(new Turret(Room.TileToWorldPos(8, 10)));
            room2.enemies.Add(new Turret(Room.TileToWorldPos(9, 11)));
            room2.enemies.Add(new Turret(Room.TileToWorldPos(9, 9)));
            room2.enemies.Add(new Turret(Room.TileToWorldPos(10, 8)));

            room2.AddPuzzle(room2.width - 2, room2.height - 2, new Portal(room3, Tile.tileSize * new Vector2(1.5f, 2.5f)));
            room2.AddPuzzle(1, 1, new Checkpoint());
            room2.AddPuzzle(room2.width - 3, room2.height - 2, new BlueDoor(false));
            room2.AddPuzzle(room2.width - 2, 2, new Switch(Room2Switch));

            // Room 3 contents
            // The room will have some enemies to fight. Ideally, when all the enemies are dead, the portal will open up. 
            room3.SetupFloorsAndWalls();

            room3.AddPuzzle(room3.width - 2, room3.height - 2, new Portal(room4, Tile.tileSize * new Vector2(1.5f, 1.5f)));
            room3.AddEnemyWithDefaultPos(new Spider(Tile.tileSize * new Vector2(10.5f, 1.5f)));
            room3.AddEnemyWithDefaultPos(new Spider(Tile.tileSize * new Vector2(10.5f, 3.5f)));
            room3.AddEnemyWithDefaultPos(new Spider(Tile.tileSize * new Vector2(15.5f, 2.5f)));
            room3.AddEnemyWithDefaultPos(new Spider(Tile.tileSize * new Vector2(20.5f, 2.5f)));
            room3.AddEnemyWithDefaultPos(new Spider(Tile.tileSize * new Vector2(25.5f, 2.5f)));
            room3.AddPuzzle(1, 2, new Checkpoint());

            // Room 4 contents
            room4.SetupFloorsAndWalls();


            // Returns the first room to be loaded
            return room1;
        }

        private static void Room2Switch(Room room, Player player)
        {
            Door door = (Door)room.tiles[room.width - 3, room.height - 2].puzzle;
            door.Open();
        }

        public override void LoadContent(ContentManager loader)
        {
            base.LoadContent(loader);
            LoadTileset("EarthDungeon", 1, 1, loader);
            Turret.LoadContent(loader);
            BrownDoor.LoadContent(loader);
            Spider.LoadContent(loader);
            BlueDoor.LoadContent(loader);
            Switch.LoadContent(loader);
        }

        public override Color BackgroundColor()
        {
            return Color.DarkGoldenrod;
        }
    }
}
