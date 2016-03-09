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
        Room room2 = new Room(30, 30);

        public override Room SetupLevel(Player player)
        {
            Vector2 startRoomStart = Room.TileToWorldPos(new Vector2(startRoom.width * 0.5f, startRoom.height - 2.5f));
            Vector2 room2Start = Room.TileToWorldPos(new Vector2(1.5f, 1.5f));

            player.position = startRoomStart;
            startRoom.FillFloor(0, startRoom.width - 1, 0, startRoom.height - 1);
            startRoom.AddWallBorder(0, startRoom.width - 1, 0, startRoom.height - 1);
            startRoom.SetWall(startRoom.width / 2, startRoom.height - 7, 2);
            startRoom.SetWall(startRoom.width / 2 - 1, startRoom.height - 6, 3);
            startRoom.SetWall(startRoom.width / 2, startRoom.height - 6, 4);
            startRoom.SetWall(startRoom.width / 2 + 1, startRoom.height - 6, 5);
            startRoom.FillWall(1, startRoom.width / 2 - 1, 4, 4);
            startRoom.FillWall(startRoom.width / 2 + 1, startRoom.width - 2, 4, 4);
            startRoom.SetWall(startRoom.width / 2, 0, 6);
            startRoom.AddPuzzle(startRoomStart, new Checkpoint());
            startRoom.AddPuzzle(startRoom.width / 2, 4, new Door(true));
            startRoom.AddPuzzle(startRoom.width / 2, 2, new Portal(room2, room2Start));

            room2.FillFloor(0, room2.width - 1, 0, room2.height - 1, 2);
            room2.AddWallBorder(0, room2.width - 1, 0, room2.height - 1);
            room2.AddPuzzle(room2Start, new Checkpoint());
            room2.AddPuzzle(room2.width - 2, room2.height - 2, new Portal(startRoom, startRoomStart));

            return startRoom;
        }

        public override void LoadContent(ContentManager loader)
        {
            base.LoadContent(loader);
            LoadTileset("Tutorial", 2, 6, loader);
            Door.LoadContent(loader);
            Turret.LoadContent(loader);
        }
    }
}
