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
        Room start = new Room(3, 3);
        Room room1 = new Room(13, 20);
        Room room2 = new Room(30, 9);
        Room room3 = new Room(30, 9);
        Room room4 = new Room(29, 29);
		MonsterRoom room4MonsterRoom;

        public override Room SetupLevel(Player player)
        {
            Vector2 room1Start = Room.TileToWorldPos(new Vector2(room1.width * 0.5f, room1.height - 2.5f));
            Vector2 room2Start = Room.TileToWorldPos(new Vector2(2.5f, room2.height * 0.5f));
            Vector2 room3Start = Room.TileToWorldPos(new Vector2(6.5f, room3.height * 0.5f));
            Vector2 room4Start = Room.TileToWorldPos(new Vector2(room4.width * 0.5f, room4.height - 2.5f));

            player.position = new Vector2(60f, 60f);
            start.SetupFloorsAndWalls();
            start.AddPuzzle(1, 1, new Portal(room4, room4Start));

            room1.SetupFloorsAndWalls();
            room1.SetWall(room1.width / 2, room1.height - 7, 2);
            room1.SetWall(room1.width / 2 - 1, room1.height - 6, 3);
            room1.SetWall(room1.width / 2, room1.height - 6, 4);
            room1.SetWall(room1.width / 2 + 1, room1.height - 6, 5);
            room1.FillWall(1, room1.width / 2 - 2, 4, 4);
            room1.FillWall(room1.width / 2 + 2, room1.width - 2, 4, 4);
            room1.SetWall(room1.width / 2 - 1, 4, 6);
            room1.SetWall(room1.width / 2 + 1, 4, 6);
            room1.AddPuzzle(room1Start, new Checkpoint());
            room1.AddPuzzle(room1.width / 2, 4, new BrownDoor(true));
            room1.AddPuzzle(room1.width / 2, 2, new Portal(room2, room2Start));

            room2.SetupFloorsAndWalls(2);
            room2.FillWall(9, room2.width - 10, room2.height - 2, room2.height - 2, 6);
            room2.AddEnemyWithDefaultPos(new Spider(Tile.tileSize * new Vector2(20.5f, room2.height * 0.5f)));
            room2.AddPuzzle(room2.width - 3, room2.height / 2, new Portal(room3, room3Start));

            room3.SetupFloorsAndWalls();
            room3.FillWall(4, 4, 1, room3.height / 2 - 1);
            room3.FillWall(4, 4, room3.height / 2 + 1, room3.height - 2);
            room3.FillWall(12, 12, 1, room3.height / 2 - 1);
            room3.FillWall(12, 12, room3.height / 2 + 1, room3.height - 2);
            room3.SetWall(room3.width - 2, room3.height / 2, 3);
            room3.enemies.Add(new Turret(Tile.tileSize * new Vector2(room3.width - 4.5f, room3.height / 2f)));
            room3.AddPuzzle(12, room3.height / 2, new Checkpoint());
            room3.AddPuzzle(2, room3.height / 2, new Portal(room4, room4Start));
            room3.AddPuzzle(4, room3.height / 2, new BlueDoor(false));
            room3.AddPuzzle(room3.width - 3, room3.height / 2, new Switch(Room3Switch));

            room4.SetupFloorsAndWalls();
            room4.FillFloor(1, 19, 1, 19, 2);
            room4.FillWall(1, 9, 20, 20);
            room4.FillWall(11, 23, 20, 20);
            room4.FillWall(20, 20, 1, 2);
            room4.FillWall(20, 20, 4, 19);
            room4.FillWall(21, 28, 6, 6);
            room4.FillWall(25, 27, 20, 20);
            room4.SetWall(10, 10, 6);
            room4.AddPuzzle(10, 24, new Checkpoint());
            room4.AddPuzzle(10, 20, new BlueDoor(true, true));
            room4.AddPuzzle(20, 3, new BlueDoor(false));
            room4.AddPuzzle(24, 3, new SilverKey());
            room4.AddPuzzle(24, 20, new SilverDoor(true));
			CreateRoom4MonsterRoom();
            Puzzle puzzle = new InvisibleSwitch(room4MonsterRoom.DoAction);
            room4.AddPuzzle(9, 19, puzzle);
            room4.AddPuzzle(10, 18, puzzle);
            room4.AddPuzzle(11, 19, puzzle);
            room4.AddPuzzle(24, 12, new Portal(room1, room1Start));
            room4.OnExit += Room4Exit;

            return start;
        }

        private static void Room3Switch(Room room, Player player)
        {
            Vector2 cameraTarget = Room.TileToWorldPos(new Vector2(4.5f, room.height / 2f));
            room.cameraEvent = new CameraEvent(cameraTarget, (Room r, Player p, int time) =>
            {
                Door door = (Door)r.tiles[4, r.height / 2].puzzle;
                door.Open();
            });
        }

        private void CreateRoom4MonsterRoom()
        {
			Vector2 cameraTarget1 = Room.TileToWorldPos(new Vector2(10.5f, 20.5f));
			MonsterRoom.CreateStartEvent createStart = (Room room, Player player) =>
			{
				List<CameraEvent> events = new List<CameraEvent>();
				events.Add(new CameraEvent(cameraTarget1, (Room r, Player p, int time) =>
				{
					Door door = (Door)r.tiles[10, 20].puzzle;
					door.Close();
				}));
				return events;
			};
            Vector2 spawnEnemyCamera = Room.TileToWorldPos(new Vector2(10.5f, 10.5f));
			MonsterRoom.CreateEnemiesEvent createEnemies = (Room r, Player p, List<Enemy> enemies) =>
			{
				enemies.Add(new Turret(Room.TileToWorldPos(new Vector2(5.5f, 7.5f))));
				enemies.Add(new Turret(Room.TileToWorldPos(new Vector2(15.5f, 7.5f))));
				enemies.Add(new Spider(Room.TileToWorldPos(new Vector2(5.5f, 13.5f))));
				enemies.Add(new Spider(Room.TileToWorldPos(new Vector2(15.5f, 13.5f))));
			};
			MonsterRoom.CreateEndEvent createEnd = (Room room, Player player) =>
			{
				List<CameraEvent> events = new List<CameraEvent>();
				events.Add(new CameraEvent(cameraTarget1, (Room r, Player p, int time) =>
				{
					Door door = (Door)r.tiles[10, 20].puzzle;
					door.Open();
				}));
				Vector2 camTarget = Room.TileToWorldPos(new Vector2(20.5f, 3.5f));
				events.Add(new CameraEvent(camTarget, (Room r, Player p, int time) =>
				{
					Door door = (Door)r.tiles[20, 3].puzzle;
					door.Open();
				}));
				return events;
			};
			room4MonsterRoom = new MonsterRoom(room4, createStart, spawnEnemyCamera, createEnemies, createEnd);
        }

        private void Room4Exit(Room room, Player player)
        {
            if (!room4MonsterRoom.cleared)
            {
                ((InvisibleSwitch)room.tiles[10, 18].puzzle).pressed = false;
                ((Door)room.tiles[10, 20].puzzle).Open();
            }
        }

        public override void LoadContent(ContentManager loader)
        {
            base.LoadContent(loader);
            LoadTileset("Tutorial", 2, 6, loader);
            BrownDoor.LoadContent(loader);
            BlueDoor.LoadContent(loader);
            SilverDoor.LoadContent(loader);
            Switch.LoadContent(loader);
            Spider.LoadContent(loader);
            Turret.LoadContent(loader);
        }
    }
}
