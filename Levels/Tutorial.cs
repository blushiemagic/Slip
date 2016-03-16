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
        Room startRoom = new Room(13, 20);
        Room room2 = new Room(30, 9);
        Room room3 = new Room(30, 9);

        public override Room SetupLevel(Player player)
        {
            Vector2 startRoomStart = Room.TileToWorldPos(new Vector2(startRoom.width * 0.5f, startRoom.height - 2.5f));
            Vector2 room2Start = Room.TileToWorldPos(new Vector2(2.5f, room2.height * 0.5f));
            Vector2 room3Start = Room.TileToWorldPos(new Vector2(6.5f, room3.height * 0.5f));

            player.position = startRoomStart;
            startRoom.SetupFloorsAndWalls();
            startRoom.SetWall(startRoom.width / 2, startRoom.height - 7, 2);
            startRoom.SetWall(startRoom.width / 2 - 1, startRoom.height - 6, 3);
            startRoom.SetWall(startRoom.width / 2, startRoom.height - 6, 4);
            startRoom.SetWall(startRoom.width / 2 + 1, startRoom.height - 6, 5);
            startRoom.FillWall(1, startRoom.width / 2 - 2, 4, 4);
            startRoom.FillWall(startRoom.width / 2 + 2, startRoom.width - 2, 4, 4);
            startRoom.SetWall(startRoom.width / 2 - 1, 4, 6);
            startRoom.SetWall(startRoom.width / 2 + 1, 4, 6);
            startRoom.AddPuzzle(startRoomStart, new Checkpoint());
            startRoom.AddPuzzle(startRoom.width / 2, 4, new BrownDoor(true));
            startRoom.AddPuzzle(startRoom.width / 2, 2, new Portal(room2, room2Start));

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
            room3.AddPuzzle(2, room3.height / 2, new Portal(startRoom, startRoomStart));
            room3.AddPuzzle(4, room3.height / 2, new BlueDoor(false));
            room3.AddPuzzle(room3.width - 3, room3.height / 2,
                new Switch(Room3Switch));

            return startRoom;
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

        public override void LoadContent(ContentManager loader)
        {
            base.LoadContent(loader);
            LoadTileset("Tutorial", 2, 6, loader);
            BrownDoor.LoadContent(loader);
            BlueDoor.LoadContent(loader);
            Switch.LoadContent(loader);
            Spider.LoadContent(loader);
            Turret.LoadContent(loader);
        }
    }
}
