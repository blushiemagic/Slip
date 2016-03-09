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

        public override Room SetupLevel(Player player)
        {
            Vector2 startRoomStart = Room.TileToWorldPos(new Vector2(startRoom.width * 0.5f, startRoom.height - 2.5f));
            Vector2 room2Start = Room.TileToWorldPos(new Vector2(2.5f, room2.height * 0.5f));

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
            startRoom.AddPuzzle(startRoom.width / 2, 4, new Door(true));
            startRoom.AddPuzzle(startRoom.width / 2, 2, new Portal(room2, room2Start));

            room2.SetupFloorsAndWalls(2);
            room2.FillWall(5, room2.width - 6, room2.height - 2, room2.height - 2, 6);
            room2.AddEnemyWithDefaultPos(new Spider(Tile.tileSize * new Vector2(20.5f, room2.height * 0.5f)));
            room2.AddPuzzle(room2.width - 3, room2.height / 2, new Portal(startRoom, startRoomStart));

            return startRoom;
        }

        public override void LoadContent(ContentManager loader)
        {
            base.LoadContent(loader);
            LoadTileset("Tutorial", 2, 6, loader);
            Door.LoadContent(loader);
            Spider.LoadContent(loader);
            Turret.LoadContent(loader);
        }
    }
}
