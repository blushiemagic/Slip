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
        Room start = new Room(3, 3);
        Room room0 = new Room(7, 15);
        Room room1 = new Room(18, 7);
        Room room2 = new Room(16, 16);
        Room room3 = new Room(20, 5);
        Room room4 = new Room(16, 16);
        Room room5 = new Room(9, 20);
        Room prepRoom = new Room(11, 11);
        Room bossRoom = new Room(20, 20);
        EarthElemental minionElemental;
        EarthBoss boss;

        public override Room SetupLevel(Player player)
        {
            Vector2 room0StartPos = Room.TileToWorldPos(new Vector2(room0.width * 0.5f, room0.height - 1.5f));
            Vector2 room2StartPos = Room.TileToWorldPos(new Vector2(room2.width - 4, room2.height - 12));
            Vector2 room4StartPos = Room.TileToWorldPos(new Vector2(1.5f, 1.5f));
            Vector2 room5StartPos = Room.TileToWorldPos(new Vector2(room5.width * 0.5f, room5.height - 1.5f));
            Vector2 prepRoomStartPos = Room.TileToWorldPos(new Vector2(prepRoom.width * 0.5f, prepRoom.height - 1.5f));
            Vector2 bossRoomStartPos = Room.TileToWorldPos(new Vector2(bossRoom.width * 0.5f, bossRoom.height - 1.5f));

            player.position = new Vector2(60f, 60f);
            start.SetupFloorsAndWalls();
            start.AddPuzzle(1, 1, new Portal(room0, room0StartPos));
            
            // Room 0 contents
            room0.SetupFloorsAndWalls();
            room0.AddPuzzle(3, 3, new BrownDoor(false));
            for (int i = 1; i < 6; i++)
            {
                if (i != 3)
                {
                    room0.tiles[i, 3].Wall = 1;
                }
            }
            room0.AddPuzzle(3, 1, new Portal(room1, Tile.tileSize * new Vector2(1.5f, 1.5f)));

            // Room 1 contents
            room1.SetupFloorsAndWalls();
            room1.AddPuzzle(4, 3, new BrownDoor(false));
            room1.tiles[4, 1].Wall = 1;
            room1.tiles[4, 2].Wall = 1;
            room1.tiles[4, 4].Wall = 1;
            room1.tiles[4, 5].Wall = 1;

            room1.enemies.Add(new Turret(Room.TileToWorldPos(7, 1)));
            room1.enemies.Add(new Turret(Room.TileToWorldPos(7, 6)));
            room1.enemies.Add(new Turret(Room.TileToWorldPos(12, 1)));
            room1.enemies.Add(new Turret(Room.TileToWorldPos(12, 6)));

            room1.enemies.Add(new Turret(Room.TileToWorldPos(17, 3)));
            room1.enemies.Add(new Turret(Room.TileToWorldPos(17, 4)));
            room1.enemies.Add(new Turret(Room.TileToWorldPos(17, 5)));
            room1.enemies.Add(new Turret(Room.TileToWorldPos(17, 2)));

            room1.AddPuzzle(15, 3, new Portal(room2, room2StartPos));
            room1.AddPuzzle(1, 1, new Checkpoint());

            // Room 2 contents
            // Ideally, we would want switches in this room to activate the portal to the next room. 
            room2.SetupFloorsAndWalls();
            room2.tiles[room2.width - 2, room2.height - 3].Wall = 1;
            room2.tiles[room2.width - 3, room2.height - 3].Wall = 1;
            for (int i = 9; i < 15; i++)
            {
                room2.tiles[i, 9].Wall = 1;
            }

            room2.enemies.Add(new Turret(Room.TileToWorldPos(8, 10)));
            room2.enemies.Add(new Turret(Room.TileToWorldPos(9, 9)));
            room2.enemies.Add(new Turret(Room.TileToWorldPos(10, 8)));
            room2.enemies.Add(new Turret(Room.TileToWorldPos(9, 7)));
            room2.enemies.Add(new Turret(Room.TileToWorldPos(6, 8)));
            room2.enemies.Add(new Turret(Room.TileToWorldPos(7, 9)));
            room2.enemies.Add(new Turret(Room.TileToWorldPos(7, 7)));
            room2.enemies.Add(new Turret(Room.TileToWorldPos(8, 6)));
            room2.enemies.Add(new Turret(Room.TileToWorldPos(8, 8)));
            room2.enemies.Add(new Turret(Room.TileToWorldPos(7, 1)));
            room2.enemies.Add(new Turret(Room.TileToWorldPos(8, 1)));
            room2.enemies.Add(new Turret(Room.TileToWorldPos(9, 1)));
            room2.enemies.Add(new Turret(Room.TileToWorldPos(7, 15)));
            room2.enemies.Add(new Turret(Room.TileToWorldPos(8, 15)));
            room2.enemies.Add(new Turret(Room.TileToWorldPos(9, 15)));
            room2.enemies.Add(new Turret(Room.TileToWorldPos(1, 7)));
            room2.enemies.Add(new Turret(Room.TileToWorldPos(1, 8)));
            room2.enemies.Add(new Turret(Room.TileToWorldPos(1, 9)));
            room2.enemies.Add(new Turret(Room.TileToWorldPos(15, 7)));
            room2.enemies.Add(new Turret(Room.TileToWorldPos(15, 8)));
            room2.enemies.Add(new Turret(Room.TileToWorldPos(15, 9)));

            room2.AddPuzzle(room2.width - 2, room2.height - 2, new Portal(room3, Tile.tileSize * new Vector2(1.5f, 2.5f)));
            room2.AddPuzzle(room2.width - 4, room2.height - 12, new Checkpoint());

            // Room 3 contents
            room3.SetupFloorsAndWalls();

            room3.AddPuzzle(room3.width - 2, room3.height - 2, new Portal(room4, room4StartPos));
            room3.AddEnemyWithDefaultPos(new EarthElemental(Room.TileToWorldPos(10, 3), 0));
            room3.AddPuzzle(1, 2, new Checkpoint());

            // Room 4 contents
            room4.SetupFloorsAndWalls();

            room4.enemies.Add(new EarthElemental(Room.TileToWorldPos(8, 7), 0));
            room4.enemies.Add(new EarthElemental(Room.TileToWorldPos(8, 9), 30));

            for (int i = 6; i < 10; i++)
            {
                room4.tiles[i, 6].Wall = 1;
                room4.tiles[i, 9].Wall = 1;
                room4.tiles[6, i].Wall = 1;
                room4.tiles[9, i].Wall = 1;
            }

            for (int i = 1; i < 13; i++)
            {
                room4.tiles[3, i].Wall = 1;
                room4.tiles[12, i].Wall = 1;
            }

            for (int i = 3; i < 15; i++)
            {
                room4.tiles[6, i].Wall = 1;
                room4.tiles[9, i].Wall = 1;
            }


            room4.AddPuzzle(1, 1, new Checkpoint());
            room4.AddPuzzle(room4.width - 2, 1, new Portal(prepRoom, prepRoomStartPos));

            // Room 5 contents
            room5.SetupFloorsAndWalls();

            // Room 6 contents
            prepRoom.SetupFloorsAndWalls();

            prepRoom.AddPuzzle(prepRoomStartPos, new Checkpoint());
            prepRoom.AddPuzzle(prepRoom.width / 2, prepRoom.height / 2, new LifeCapsule());
            prepRoom.AddPuzzle(prepRoom.width / 2, 1, new Portal(bossRoom, bossRoomStartPos));

            // Boss room contents
            bossRoom.SetupFloorsAndWalls();

            bossRoom.OnEnter += BossRoomEnter;
            boss = new EarthBoss(Tile.tileSize * new Vector2(bossRoom.width * 0.5f, 2.5f),
                Tile.tileSize * 0.5f * new Vector2(bossRoom.width, bossRoom.height));
            minionElemental = new EarthElemental(Room.TileToWorldPos(10, 10), 0);
            boss.OnDeath += Win;
            bossRoom.enemies.Add(boss);
            bossRoom.enemies.Add(minionElemental);
            for (int i = 8; i < 12; i++)
            {
                bossRoom.SetWall(i, 8, 1);
                bossRoom.SetWall(i, 11, 1);
                bossRoom.SetWall(8, i, 1);
                bossRoom.SetWall(11, i, 1);
            }


            // Returns the first room to be loaded
            return start;
        }

        // All helper functions used in this dungeon

        private void BossRoomEnter(Room room, Player player)
        {
            room.cameraEvent = new CameraEvent(
                Room.TileToWorldPos(new Vector2(room.width * 0.5f, 2.5f)),
                (Room r, Player p, int time) => { }, 30);
            boss.Reset();
        }

        private void Win(Enemy enemy, Room room, Player player)
        {
            room.bullets.Clear();
            player.exp++;
            minionElemental.Kill(room, player);
            room.AddPuzzle(room.width / 2, 2, new Goal("Earth Dungeon", Color.DarkGoldenrod));
        }

        // Load images
        public override void LoadContent(ContentManager loader)
        {
            base.LoadContent(loader);
            LoadTileset("EarthDungeon", 1, 1, loader);
            Turret.LoadContent(loader);
            BrownDoor.LoadContent(loader);
            Spider.LoadContent(loader);
            BlueDoor.LoadContent(loader);
            Switch.LoadContent(loader);
            EarthElemental.LoadContent(loader);
            LifeCapsule.LoadContent(loader);
            EarthBoss.LoadContent(loader);
        }

        public override Color BackgroundColor()
        {
            return Color.DarkGoldenrod;
        }
    }
}
