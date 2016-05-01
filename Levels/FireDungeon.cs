using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Slip.Enemies;
using Slip.Puzzles;

namespace Slip.Levels
{
    public class FireDungeon : GameScreen
    {
        Room start = new Room(3, 3);
        Room bottom = new Room(17, 20);
        Room center = new Room(21, 21);
        Room topLeft = new Room(22, 22); //lava path
        Room topRight = new Room(17, 57); //monster room
        Room bottomLeft = new Room(20, 16); //timing
        Room bottomRight = new Room(21, 21); //switches
        Room top = new Room(17, 25);
        Room rest = new Room(13, 13);
        Room bossRoom = new Room(30, 30);
        MonsterRoom monsterRoom1;
        MonsterRoom monsterRoom2;
        MonsterRoom monsterRoom3;
        MonsterRoom topMonsterRoom;

        public override Room SetupLevel(Player player)
        {
            Vector2 bottomStart = Room.TileToWorldPos(new Vector2(bottom.width * 0.5f, bottom.height - 2.5f));
            Vector2 bottomEnd = Room.TileToWorldPos(new Vector2(bottom.width * 0.5f, 3.5f));
            Vector2 centerBottom = Room.TileToWorldPos(new Vector2(center.width * 0.5f, center.height - 2.5f));
            Vector2 centerTopLeft = Room.TileToWorldPos(new Vector2(2.5f, 2.5f));
            Vector2 topLeftStart = Room.TileToWorldPos(new Vector2(topLeft.width - 2.5f, topLeft.height - 2.5f));
            Vector2 centerTopRight = Room.TileToWorldPos(new Vector2(center.width - 2.5f, 2.5f));
            Vector2 topRightStart = Room.TileToWorldPos(new Vector2(topRight.width * 0.5f, topRight.height - 2.5f));
            Vector2 centerBottomLeft = Room.TileToWorldPos(new Vector2(2.5f, center.height - 2.5f));
            Vector2 bottomLeftStart = Room.TileToWorldPos(new Vector2(bottomLeft.width - 2.5f, bottomLeft.height - 2.5f));
            Vector2 centerBottomRight = Room.TileToWorldPos(new Vector2(center.width - 2.5f, center.height - 2.5f));
            Vector2 bottomRightStart = Room.TileToWorldPos(new Vector2(2.5f, bottomRight.height - 2.5f));
            Vector2 centerTop = Room.TileToWorldPos(new Vector2(center.width * 0.5f, 2.5f));
            Vector2 topStart = Room.TileToWorldPos(new Vector2(top.width * 0.5f, top.height - 2.5f));
            Vector2 topEnd = Room.TileToWorldPos(new Vector2(top.width * 0.5f, 2.5f));
            Vector2 restStart = Room.TileToWorldPos(new Vector2(rest.width * 0.5f, rest.height - 2.5f));
            Vector2 bossStart = Room.TileToWorldPos(new Vector2(bossRoom.width * 0.5f, bossRoom.height - 2.5f));

            player.position = new Vector2(60f, 60f);
            start.SetupFloorsAndWalls();
            start.AddPuzzle(1, 1, new Portal(rest, restStart));

            bottom.SetupFloorsAndWalls();
            bottom.AddPuzzle(bottomStart, new Checkpoint());
            bottom.FillFloor(6, 10, 1, 18, 2);
            for (int k = 1; k < bottom.height - 1; k++)
            {
                if (k == 4)
                {
                    continue;
                }
                bottom.AddPuzzle(1, k, new Lava());
                bottom.AddPuzzle(2, k, new Lava());
                bottom.AddPuzzle(bottom.width - 2, k, new Lava());
                bottom.AddPuzzle(bottom.width - 3, k, new Lava());
            }
            bottom.FillWall(1, 7, 4, 4);
            bottom.FillWall(9, 15, 4, 4);
            bottom.AddPuzzle(8, 4, new BrownDoor(true));
            bottom.AddPuzzle(8, 2, new Portal(center, centerBottom));

            center.SetupFloorsAndWalls(2);
            center.AddPuzzle(center.width / 2, center.height - 2, new Portal(bottom, bottomEnd));
            for (int i = center.width / 4; i <= 3 * center.width / 4; i += center.width / 2)
            {
                for (int j = center.height / 4; j <= 3 * center.height / 4; j += center.height / 2)
                {
                    for (int x = -1; x <= 1; x++)
                    {
                        for (int y = -1; y <= 1; y++)
                        {
                            center.AddPuzzle(i + x, j + y, new Lava());
                        }
                    }
                    center.FillFloor(i - 3, i + 3, j - 3, j + 3);
                }
            }
            center.AddPuzzle(center.width / 2, center.height / 2, new Checkpoint());
            center.AddPuzzle(1, 1, new Portal(topLeft, topLeftStart));
            center.AddPuzzle(center.width - 2, 1, new Portal(topRight, topRightStart));
            center.AddPuzzle(1, center.height - 2, new Portal(bottomLeft, bottomLeftStart));
            center.AddPuzzle(center.width - 2, center.height - 2, new Portal(bottomRight, bottomRightStart));
            center.FillWall(9, 9, 1, 8);
            center.FillWall(11, 11, 1, 8);
            for (int k = 2; k <= 8; k += 2)
            {
                center.AddPuzzle(center.width / 2, k, new SilverDoor(true));
            }
            center.AddPuzzle(center.width / 2, 1, new Portal(top, topStart));

            #region topLeft
            topLeft.SetupFloorsAndWalls(2);
            for (int x = 1; x <= 20; x++)
            {
                for (int y = 1; y <= 20; y++)
                {
                    topLeft.AddPuzzle(x, y, new Lava());
                }
            }
            for (int x = 18; x <= 20; x++)
            {
                for (int y = 18; y <= 20; y++)
                {
                    topLeft.RemovePuzzle(x, y);
                }
            }
            topLeft.AddPuzzle(20, 20, new Portal(center, centerTopLeft));
            topLeft.AddPuzzle(19, 19, new Checkpoint());
            for (int x = 14; x <= 17; x++)
            {
                topLeft.RemovePuzzle(x, 19);
            }
            topLeft.RemovePuzzle(14, 18);
            for (int x = 5; x <= 14; x++)
            {
                topLeft.RemovePuzzle(x, 17);
            }
            for (int x = 7; x <= 11; x++)
            {
                topLeft.RemovePuzzle(x, 20);
            }
            for (int x = 7; x <= 11; x += 2)
            {
                FixedTurret turret = new FixedTurret(Room.TileToWorldPos(new Vector2(x + 0.5f, 20.5f)),
                    new Vector2(0f, -4f));
                turret.bulletTime = 240;
                topLeft.enemies.Add(turret);
            }
            topLeft.AddPuzzle(5, 17, new Switch(TopLeftSwitch1));
            topLeft.AddPuzzle(12, 16, new BlueDoor(true));
            for (int x = 3; x <= 12; x++)
            {
                topLeft.RemovePuzzle(x, 15);
            }
            for (int y = 15; y <= 17; y++)
            {
                topLeft.RemovePuzzle(2, y);
            }
            for (int y = 17; y <= 19; y++)
            {
                topLeft.RemovePuzzle(3, y);
            }
            topLeft.AddPuzzle(2, 19, new Switch(TopLeftSwitch2));
            topLeft.AddPuzzle(5, 14, new BlueDoor(true));
            for (int x = 3; x <= 5; x++)
            {
                topLeft.RemovePuzzle(x, 13);
                topLeft.RemovePuzzle(x, 11);
                topLeft.RemovePuzzle(x, 9);
                topLeft.RemovePuzzle(x, 7);
            }
            topLeft.RemovePuzzle(3, 12);
            topLeft.RemovePuzzle(5, 10);
            topLeft.RemovePuzzle(3, 8);
            for (int y = 7; y <= 13; y++)
            {
                topLeft.RemovePuzzle(1, y);
            }
            for (int y = 8; y <= 12; y += 2)
            {
                FixedTurret turret = new FixedTurret(Room.TileToWorldPos(new Vector2(1.5f, y + 0.5f)),
                    new Vector2(4f, 0f));
                turret.bulletTime = 240;
                topLeft.enemies.Add(turret);
            }
            topLeft.RemovePuzzle(5, 6);
            topLeft.RemovePuzzle(5, 5);
            topLeft.RemovePuzzle(6, 5);
            for (int y = 3; y <= 5; y++)
            {
                topLeft.RemovePuzzle(7, y);
                topLeft.RemovePuzzle(9, y);
                topLeft.RemovePuzzle(11, y);
            }
            topLeft.RemovePuzzle(6, 5);
            topLeft.RemovePuzzle(8, 3);
            topLeft.RemovePuzzle(10, 5);
            for (int x = 11; x <= 19; x++)
            {
                topLeft.RemovePuzzle(x, 3);
            }
            for (int y = 4; y <= 8; y++)
            {
                topLeft.RemovePuzzle(15, y);
            }
            topLeft.AddPuzzle(16, 8, new Switch(TopLeftSwitch3));
            for (int y = 4; y <= 17; y++)
            {
                topLeft.RemovePuzzle(19, y);
            }
            topLeft.AddPuzzle(19, 6, new BlueDoor(true));
            for (int y = 6; y <= 17; y++)
            {
                topLeft.RemovePuzzle(18, y);
                topLeft.RemovePuzzle(20, y);
            }
            topLeft.FillWall(18, 18, 6, 12);
            topLeft.FillWall(20, 20, 6, 12);
            for (int y = 12; y <= 18; y++)
            {
                topLeft.RemovePuzzle(17, y);
            }
            topLeft.FillWall(17, 17, 12, 18);
            topLeft.SetWall(18, 16, 1);
            topLeft.SetWall(18, 17, 1);
            topLeft.SetWall(20, 16, 1);
            topLeft.SetWall(20, 17, 1);
            topLeft.RemovePuzzle(17, 20);
            topLeft.SetWall(17, 20, 1);
            topLeft.AddPuzzle(19, 14, new SilverKey());
            topLeft.AddPuzzle(19, 16, new Switch(TopLeftSwitch4));
            topLeft.AddPuzzle(19, 17, new BlueDoor(true));
            topLeft.AddPuzzle(14, 7, new BlueDoor(false));
            topLeft.RemovePuzzle(13, 7);
            topLeft.AddPuzzle(12, 7, new Checkpoint());
            topLeft.RemovePuzzle(12, 8);
            for (int x = 7; x <= 11; x++)
            {
                for (int y = 8; y <= 12; y++)
                {
                    if (x % 2 == 1 || y % 2 == 0)
                    {
                        topLeft.RemovePuzzle(x, y);
                    }
                }
            }
            topLeft.RemovePuzzle(10, 9);
            topLeft.AddPuzzle(7, 12, new LifeCapsule());
            topLeft.OnExit += TopLeftExit;
            #endregion

            #region topRight
            topRight.SetupFloorsAndWalls();
            topRight.AddPuzzle(topRight.width / 2, topRight.height - 2, new Portal(center, centerTopRight));
            topRight.AddPuzzle(topRight.width / 2, topRight.height - 3, new Checkpoint());
            for (int x = 1; x <= 3; x++)
            {
                for (int y = 53; y <= 55; y++)
                {
                    topRight.AddPuzzle(x, y, new Lava());
                }
            }
            for (int x = 13; x <= 15; x++)
            {
                for (int y = 53; y <= 55; y++)
                {
                    topRight.AddPuzzle(x, y, new Lava());
                }
            }
            topRight.FillWall(1, 7, 52, 52);
            topRight.AddPuzzle(8, 52, new BlueDoor(true, true));
            topRight.FillWall(9, 15, 52, 52);
            MakeMonsterRoom1();
            Puzzle puzzle = new InvisibleSwitch(monsterRoom1.DoAction);
            topRight.AddPuzzle(7, 51, puzzle);
            topRight.AddPuzzle(8, 50, puzzle);
            topRight.AddPuzzle(9, 51, puzzle);
            topRight.FillFloor(4, 12, 40, 48, 2);
            topRight.FillWall(1, 7, 36, 36);
            topRight.AddPuzzle(8, 36, new BlueDoor(true, true));
            topRight.FillWall(9, 15, 36, 36);
            MakeMonsterRoom2();
            puzzle = new InvisibleSwitch(monsterRoom2.DoAction);
            topRight.AddPuzzle(7, 35, puzzle);
            topRight.AddPuzzle(8, 34, puzzle);
            topRight.AddPuzzle(9, 35, puzzle);
            topRight.FillFloor(4, 12, 24, 32, 2);
            topRight.FillWall(1, 7, 20, 20);
            topRight.AddPuzzle(8, 20, new BlueDoor(true, true));
            topRight.FillWall(9, 15, 20, 20);
            MakeMonsterRoom3();
            puzzle = new InvisibleSwitch(monsterRoom3.DoAction);
            topRight.AddPuzzle(7, 19, puzzle);
            topRight.AddPuzzle(8, 18, puzzle);
            topRight.AddPuzzle(9, 19, puzzle);
            topRight.FillFloor(4, 12, 8, 16, 2);
            topRight.FillWall(1, 7, 4, 4);
            topRight.AddPuzzle(8, 4, new BlueDoor(true, true));
            topRight.FillWall(9, 15, 4, 4);
            for (int x = 1; x <= 3; x++)
            {
                for (int y = 1; y <= 3; y++)
                {
                    topRight.AddPuzzle(x, y, new Lava());
                }
            }
            topRight.AddPuzzle(8, 2, new SilverKey());
            for (int x = 13; x <= 15; x++)
            {
                for (int y = 1; y <= 3; y++)
                {
                    topRight.AddPuzzle(x, y, new Lava());
                }
            }
            topRight.OnExit += TopRightExit;
            #endregion topRight

            #region bottomLeft
            bottomLeft.SetupFloorsAndWalls(2);
            bottomLeft.FillFloor(1, 15, 1, 9);
            bottomLeft.AddPuzzle(bottomLeft.width - 2, bottomLeft.height - 2, new Portal(center, centerBottomLeft));
            bottomLeft.AddPuzzle(bottomLeft.width - 3, bottomLeft.height - 3, new Checkpoint());
            bottomLeft.FillWall(1, 3, 1, 2);
            bottomLeft.FillWall(6, 7, 1, 2);
            bottomLeft.FillWall(10, 11, 1, 2);
            bottomLeft.FillWall(14, bottomLeft.width - 1, 1, 2);
            Boulder.Add(bottomLeft, 4, 3);
            Boulder.Add(bottomLeft, 8, 3);
            Boulder.Add(bottomLeft, 12, 3);
            bottomLeft.FillWall(1, 3, 5, 5);
            bottomLeft.FillWall(6, 7, 5, 5);
            bottomLeft.FillWall(10, 11, 5, 5);
            bottomLeft.FillWall(14, 15, 5, 5);
            for (int x = 1; x <= 15; x++)
            {
                for (int y = 6; y <= 7; y++)
                {
                    bottomLeft.AddPuzzle(x, y, new Lava());
                }
            }
            bottomLeft.FillWall(1, 8, 10, 10);
            bottomLeft.AddPuzzle(9, 10, new Lever(Direction.Left, BottomLeftLever));
            bottomLeft.FillWall(10, 15, 10, 10);
            bottomLeft.FillWall(16, 16, 5, 10);
            bottomLeft.AddPuzzle(1, 4, new SilverKey());
            bottomLeft.enemies.Add(new Cannon(Room.TileToWorldPos(new Vector2(2f, 9f)), new Vector2(4f, 0f)));
            bottomLeft.AddEnemyWithDefaultPos(new FireWizard(Room.TileToWorldPos(
                new Vector2(3.5f, bottomLeft.height - 3f))));
            #endregion

            #region bottomRight
            bottomRight.SetupFloorsAndWalls();
            bottomRight.FillFloor(3, bottomRight.width - 4, 3, bottomRight.height - 4, 2);
            bottomRight.AddPuzzle(1, bottomRight.height - 2, new Portal(center, centerBottomRight));
            bottomRight.AddPuzzle(2, bottomRight.height - 3, new Checkpoint());
            bottomRight.FillWall(bottomRight.width - 4, bottomRight.width - 2,
                bottomRight.height - 3, bottomRight.height - 3);
            bottomRight.AddPuzzle(bottomRight.width - 4, bottomRight.height - 2, new BlueDoor(false));
            bottomRight.AddPuzzle(bottomRight.width - 2, bottomRight.height - 2, new SilverKey());
            for (int x = 5; x <= 15; x += 5)
            {
                for (int y = 5; y <= 15; y += 5)
                {
                    bottomRight.AddPuzzle(x, y, new Lever(BottomRightLever(x, y)));
                    bottomRight.AddPuzzle(x, y - 1, new LightBulb());
                }
            }
            bottomRight.AddEnemyWithDefaultPos(new FireWizard(Room.TileToWorldPos(
                new Vector2(8.5f, bottomRight.height - 8.5f))));
            #endregion

            #region top
            top.SetupFloorsAndWalls();
            top.AddPuzzle(top.width / 2, top.height - 2, new Portal(center, centerTop));
            top.AddPuzzle(top.width / 2, top.height - 3, new Checkpoint());
            for (int x = 1; x <= 3; x++)
            {
                for (int y = top.height - 4; y <= top.height - 2; y++)
                {
                    top.AddPuzzle(x, y, new Lava());
                }
            }
            for (int x = 13; x <= 15; x++)
            {
                for (int y = top.height - 4; y <= top.height - 2; y++)
                {
                    top.AddPuzzle(x, y, new Lava());
                }
            }
            top.FillWall(1, 7, top.height - 5, top.height - 5);
            top.AddPuzzle(8, top.height - 5, new BlueDoor(true, true));
            top.FillWall(9, 15, top.height - 5, top.height - 5);
            MakeTopMonsterRoom();
            puzzle = new InvisibleSwitch(topMonsterRoom.DoAction);
            top.AddPuzzle(7, top.height - 6, puzzle);
            top.AddPuzzle(8, top.height - 7, puzzle);
            top.AddPuzzle(9, top.height - 6, puzzle);
            top.FillFloor(4, 12, 8, top.height - 9, 2);
            top.FillWall(1, 7, 4, 4);
            top.AddPuzzle(8, 4, new BlueDoor(true, true));
            top.FillWall(9, 15, 4, 4);
            for (int x = 1; x <= 3; x++)
            {
                for (int y = 1; y <= 3; y++)
                {
                    top.AddPuzzle(x, y, new Lava());
                }
            }
            top.AddPuzzle(8, 1, new Portal(rest, restStart));
            for (int x = 13; x <= 15; x++)
            {
                for (int y = 1; y <= 3; y++)
                {
                    top.AddPuzzle(x, y, new Lava());
                }
            }
            top.OnExit += TopExit;
            #endregion top

            rest.SetupFloorsAndWalls();
            rest.FillFloor(5, 7, 1, rest.height - 2, 2);
            rest.AddPuzzle(rest.width / 2, rest.height - 2, new Portal(top, topEnd));
            rest.AddPuzzle(rest.width / 2, rest.height - 3, new Checkpoint());
            for (int k = 3; k <= 9; k += 2)
            {
                rest.SetWall(3, k, 1);
                rest.SetWall(rest.width - 4, k, 1);
            }
            rest.AddPuzzle(rest.width / 2, rest.height / 2, new LifeCapsule());
            rest.AddPuzzle(rest.width / 2, 2, new Portal(bossRoom, bossStart));

            bossRoom.SetupFloorsAndWalls();
            bossRoom.OnEnter += BossRoomEnter;

            return start;
        }

        #region topLeftHelper
        private void TopLeftSwitch1(Room room, Player player)
        {
            ((Door)room.tiles[12, 16].puzzle).Open();
        }

        private void TopLeftSwitch2(Room room, Player player)
        {
            ((Door)room.tiles[5, 14].puzzle).Open();
        }

        private void TopLeftSwitch3(Room room, Player player)
        {
            ((Door)room.tiles[19, 6].puzzle).Open();
        }

        private void TopLeftSwitch4(Room room, Player player)
        {
            ((Door)room.tiles[19, 17].puzzle).Open();
            ((Door)room.tiles[14, 7].puzzle).Open();
        }

        private void TopLeftExit(Room room, Player player)
        {
            if (!((Switch)room.tiles[19, 16].puzzle).pressed)
            {
                ((Switch)room.tiles[5, 17].puzzle).pressed = false;
                ((Door)room.tiles[12, 16].puzzle).Close();
                ((Switch)room.tiles[2, 19].puzzle).pressed = false;
                ((Door)room.tiles[5, 14].puzzle).Close();
                ((Switch)room.tiles[16, 8].puzzle).pressed = false;
                ((Door)room.tiles[19, 6].puzzle).Close();
            }
        }
        #endregion topLeftHelper

        #region topRightHelper
        private void MakeMonsterRoom1()
        {
            Vector2 door1Camera = Room.TileToWorldPos(new Vector2(8.5f, 52.5f));
            Vector2 door2Camera = Room.TileToWorldPos(new Vector2(8.5f, 36.5f));
            MonsterRoom.CreateStartEvent createStart = (Room room, Player player) =>
            {
                List<CameraEvent> events = new List<CameraEvent>();
                events.Add(new CameraEvent(door1Camera, (Room r, Player p, int time) =>
                {
                    ((Door)room.tiles[8, 52].puzzle).Close();
                }));
                events.Add(new CameraEvent(door2Camera, (Room r, Player p, int time) =>
                {
                    ((Door)room.tiles[8, 36].puzzle).Close();
                }));
                return events;
            };
            Vector2 spawnEnemyCamera = (door1Camera + door2Camera) / 2f;
            MonsterRoom.CreateEnemiesEvent createEnemies = (Room room, Player player, List<Enemy> enemies) =>
            {
                Turret turret = new Turret(spawnEnemyCamera);
                turret.maxShootTimer = 75;
                enemies.Add(turret);
                enemies.Add(new Spider(spawnEnemyCamera - Room.TileToWorldPos(-4, -4)));
                enemies.Add(new Spider(spawnEnemyCamera - Room.TileToWorldPos(-5, 0)));
                enemies.Add(new Spider(spawnEnemyCamera - Room.TileToWorldPos(-4, 4)));
                enemies.Add(new Spider(spawnEnemyCamera - Room.TileToWorldPos(4, -4)));
                enemies.Add(new Spider(spawnEnemyCamera - Room.TileToWorldPos(5, 0)));
                enemies.Add(new Spider(spawnEnemyCamera - Room.TileToWorldPos(4, 4)));
            };
            MonsterRoom.CreateEndEvent createEnd = (Room room, Player player) =>
            {
                List<CameraEvent> events = new List<CameraEvent>();
                events.Add(new CameraEvent(door1Camera, (Room r, Player p, int time) =>
                {
                    ((Door)room.tiles[8, 52].puzzle).Open();
                }));
                events.Add(new CameraEvent(door2Camera, (Room r, Player p, int time) =>
                {
                    ((Door)room.tiles[8, 36].puzzle).Open();
                }));
                return events;
            };
            monsterRoom1 = new MonsterRoom(topRight, createStart, spawnEnemyCamera, createEnemies, createEnd);
        }

        private void MakeMonsterRoom2()
        {
            Vector2 door1Camera = Room.TileToWorldPos(new Vector2(8.5f, 36.5f));
            Vector2 door2Camera = Room.TileToWorldPos(new Vector2(8.5f, 20.5f));
            MonsterRoom.CreateStartEvent createStart = (Room room, Player player) =>
            {
                List<CameraEvent> events = new List<CameraEvent>();
                events.Add(new CameraEvent(door1Camera, (Room r, Player p, int time) =>
                {
                    ((Door)room.tiles[8, 36].puzzle).Close();
                }));
                events.Add(new CameraEvent(door2Camera, (Room r, Player p, int time) =>
                {
                    ((Door)room.tiles[8, 20].puzzle).Close();
                }));
                return events;
            };
            Vector2 spawnEnemyCamera = (door1Camera + door2Camera) / 2f;
            MonsterRoom.CreateEnemiesEvent createEnemies = (Room room, Player player, List<Enemy> enemies) =>
            {
                FireCaster enemy = new FireCaster(spawnEnemyCamera + Room.TileToWorldPos(-5, -3));
                enemy.maxShootTimer = 90;
                enemies.Add(enemy);
                enemy = new FireCaster(spawnEnemyCamera + Room.TileToWorldPos(5, -3));
                enemy.maxShootTimer = 90;
                enemies.Add(enemy);
            };
            MonsterRoom.CreateEndEvent createEnd = (Room room, Player player) =>
            {
                List<CameraEvent> events = new List<CameraEvent>();
                events.Add(new CameraEvent(door1Camera, (Room r, Player p, int time) =>
                {
                    ((Door)room.tiles[8, 36].puzzle).Open();
                }));
                events.Add(new CameraEvent(door2Camera, (Room r, Player p, int time) =>
                {
                    ((Door)room.tiles[8, 20].puzzle).Open();
                }));
                return events;
            };
            monsterRoom2 = new MonsterRoom(topRight, createStart, spawnEnemyCamera, createEnemies, createEnd);
        }

        private void MakeMonsterRoom3()
        {
            Vector2 door1Camera = Room.TileToWorldPos(new Vector2(8.5f, 20.5f));
            Vector2 door2Camera = Room.TileToWorldPos(new Vector2(8.5f, 4.5f));
            MonsterRoom.CreateStartEvent createStart = (Room room, Player player) =>
            {
                List<CameraEvent> events = new List<CameraEvent>();
                events.Add(new CameraEvent(door1Camera, (Room r, Player p, int time) =>
                {
                    ((Door)room.tiles[8, 20].puzzle).Close();
                }));
                events.Add(new CameraEvent(door2Camera, (Room r, Player p, int time) =>
                {
                    ((Door)room.tiles[8, 4].puzzle).Close();
                }));
                return events;
            };
            Vector2 spawnEnemyCamera = (door1Camera + door2Camera) / 2f;
            MonsterRoom.CreateEnemiesEvent createEnemies = (Room room, Player player, List<Enemy> enemies) =>
            {
                Turret turret = new Turret(spawnEnemyCamera);
                turret.maxShootTimer = 120;
                enemies.Add(turret);
                enemies.Add(new Spider(spawnEnemyCamera + Room.TileToWorldPos(-5, 1)));
                enemies.Add(new Spider(spawnEnemyCamera + Room.TileToWorldPos(5, 1)));
                FireCaster caster = new FireCaster(spawnEnemyCamera + Room.TileToWorldPos(-2, -4));
                caster.maxShootTimer = 120;
                enemies.Add(caster);
                caster = new FireCaster(spawnEnemyCamera + Room.TileToWorldPos(2, -4));
                caster.maxShootTimer = 120;
                enemies.Add(caster);
            };
            MonsterRoom.CreateEndEvent createEnd = (Room room, Player player) =>
            {
                List<CameraEvent> events = new List<CameraEvent>();
                events.Add(new CameraEvent(door1Camera, (Room r, Player p, int time) =>
                {
                    ((Door)room.tiles[8, 20].puzzle).Open();
                }));
                events.Add(new CameraEvent(door2Camera, (Room r, Player p, int time) =>
                {
                    ((Door)room.tiles[8, 4].puzzle).Open();
                }));
                return events;
            };
            monsterRoom3 = new MonsterRoom(topRight, createStart, spawnEnemyCamera, createEnemies, createEnd);
        }

        private void TopRightExit(Room room, Player player)
        {
            ((Door)room.tiles[8, 52].puzzle).Open();
            ((Door)room.tiles[8, 36].puzzle).Open();
            ((Door)room.tiles[8, 20].puzzle).Open();
            ((Door)room.tiles[8, 4].puzzle).Open();
            if (!monsterRoom1.cleared)
            {
                ((InvisibleSwitch)room.tiles[8, 50].puzzle).pressed = false;
            }
            if (!monsterRoom2.cleared)
            {
                ((InvisibleSwitch)room.tiles[8, 34].puzzle).pressed = false;
            }
            if (!monsterRoom3.cleared)
            {
                ((InvisibleSwitch)room.tiles[8, 18].puzzle).pressed = false;
            }
        }
        #endregion topRightHelper

        #region bottomLeftHelper
        private static void BottomLeftLever(Room room, Player player)
        {
            foreach (Enemy enemy in room.enemies)
            {
                if (enemy is Cannon)
                {
                    ((Cannon)enemy).Fire(room);
                }
            }
        }
        #endregion

        #region bottomRightHelper
        private static Switch.SwitchAction BottomRightLever(int x, int y)
        {
            return (Room room, Player player) =>
            {
                if (y > 5)
                {
                    ((LightBulb)room.tiles[x, y - 6].puzzle).Switch();
                }
                if (x > 5)
                {
                    ((LightBulb)room.tiles[x - 5, y - 1].puzzle).Switch();
                }
                ((LightBulb)room.tiles[x, y - 1].puzzle).Switch();
                if (x < 15)
                {
                    ((LightBulb)room.tiles[x + 5, y - 1].puzzle).Switch();
                }
                if (y < 15)
                {
                    ((LightBulb)room.tiles[x, y + 4].puzzle).Switch();
                }
                bool flag = true;
                for (int i = 5; i <= 15; i += 5)
                {
                    for (int j = 4; j <= 14; j += 5)
                    {
                        LightBulb lightBulb = room.tiles[i, j].puzzle as LightBulb;
                        if (lightBulb != null && !lightBulb.light)
                        {
                            flag = false;
                        }
                    }
                }
                Door door = (Door)room.tiles[room.width - 4, room.height - 2].puzzle;
                if (flag && !door.open)
                {
                    room.cameraEvent = new CameraEvent(Room.TileToWorldPos(
                        new Vector2(room.width - 3.5f, room.height - 1.5f)),
                        (Room r, Player p, int time) =>
                        {
                            door.Open();
                        });
                }
            };
        }
        #endregion

        #region topHelper
        private void MakeTopMonsterRoom()
        {
            Vector2 door1Camera = Room.TileToWorldPos(new Vector2(top.width * 0.5f, top.height - 4.5f));
            Vector2 door2Camera = Room.TileToWorldPos(new Vector2(top.width * 0.5f, 4.5f));
            MonsterRoom.CreateStartEvent createStart = (Room room, Player player) =>
            {
                List<CameraEvent> events = new List<CameraEvent>();
                events.Add(new CameraEvent(door1Camera, (Room r, Player p, int time) =>
                {
                    ((Door)room.tiles[top.width / 2, top.height - 5].puzzle).Close();
                }));
                events.Add(new CameraEvent(door2Camera, (Room r, Player p, int time) =>
                {
                    ((Door)room.tiles[top.width / 2, 4].puzzle).Close();
                }));
                return events;
            };
            Vector2 spawnEnemyCamera = (door1Camera + door2Camera) / 2f;
            MonsterRoom.CreateEnemiesEvent createEnemies = (Room room, Player player, List<Enemy> enemies) =>
            {
                enemies.Add(new FireWizard(spawnEnemyCamera));
                FireCaster caster = new FireCaster(spawnEnemyCamera + Room.TileToWorldPos(-5, 0));
                caster.maxShootTimer = 90;
                enemies.Add(caster);
                caster = new FireCaster(spawnEnemyCamera + Room.TileToWorldPos(0, -5));
                caster.maxShootTimer = 90;
                enemies.Add(caster);
                caster = new FireCaster(spawnEnemyCamera + Room.TileToWorldPos(5, 0));
                caster.maxShootTimer = 90;
                enemies.Add(caster);
            };
            MonsterRoom.CreateEndEvent createEnd = (Room room, Player player) =>
            {
                List<CameraEvent> events = new List<CameraEvent>();
                events.Add(new CameraEvent(door1Camera, (Room r, Player p, int time) =>
                {
                    ((Door)room.tiles[top.width / 2, top.height - 5].puzzle).Open();
                }));
                events.Add(new CameraEvent(door2Camera, (Room r, Player p, int time) =>
                {
                    ((Door)room.tiles[top.width / 2, 4].puzzle).Open();
                }));
                return events;
            };
            topMonsterRoom = new MonsterRoom(top, createStart, spawnEnemyCamera, createEnemies, createEnd);
        }

        private void TopExit(Room room, Player player)
        {
            ((Door)room.tiles[room.width / 2, room.height - 5].puzzle).Open();
            ((Door)room.tiles[room.width / 2, 4].puzzle).Open();
            if (!topMonsterRoom.cleared)
            {
                ((InvisibleSwitch)room.tiles[8, top.height - 7].puzzle).pressed = false;
            }
        }
        #endregion topHelper

        #region bossHelper
        private void ResetBossRoom()
        {
            for (int x = 0; x < bossRoom.width; x++)
            {
                for (int y = 0; y < bossRoom.height; y++)
                {
                    bossRoom.RemovePuzzle(x, y);
                }
            }
            for (int x = bossRoom.width / 2 - 2; x < bossRoom.width / 2 + 2; x++)
            {
                bossRoom.AddPuzzle(x, bossRoom.height / 2 - 2, new Lava());
                bossRoom.AddPuzzle(x, bossRoom.height / 2 + 1, new Lava());
            }
            for (int y = bossRoom.height / 2 - 1; y < bossRoom.height / 2 + 1; y++)
            {
                bossRoom.AddPuzzle(bossRoom.width / 2 - 2, y, new Lava());
                bossRoom.AddPuzzle(bossRoom.width / 2 + 1, y, new Lava());
            }
            Boulder.Add(bossRoom, bossRoom.width / 2 - 1, bossRoom.height / 2 - 9);
            Boulder.Add(bossRoom, bossRoom.width / 2 - 9, bossRoom.height / 2 - 1);
            Boulder.Add(bossRoom, bossRoom.width / 2 + 7, bossRoom.height / 2 - 1);
            Boulder.Add(bossRoom, bossRoom.width / 2 - 1, bossRoom.height / 2 + 7);
        }

        private void BossRoomEnter(Room room, Player player)
        {
            room.cameraEvent = new CameraEvent(
                Room.TileToWorldPos(new Vector2(room.width * 0.5f, room.height * 0.5f)),
                (Room r, Player p, int time) => { }, 30);
            ResetBossRoom();
        }

        private void Win(Enemy enemy, Room room, Player player)
        {
            room.bullets.Clear();
            for (int x = 0; x < room.width; x++)
            {
                for (int y = 0; y < room.height; y++)
                {
                    room.RemovePuzzle(x, y);
                }
            }
            player.exp++;
            room.AddPuzzle(room.width / 2, room.height / 2, new Goal("Fire Dungeon", Color.Brown));
        }
        #endregion

        public override void LoadContent(ContentManager loader)
        {
            base.LoadContent(loader);
            LoadTileset("FireDungeon", 2, 1, loader);
            BrownDoor.LoadContent(loader);
            BlueDoor.LoadContent(loader);
            SilverDoor.LoadContent(loader);
            Switch.LoadContent(loader);
            Lava.LoadContent(loader);
            Lever.LoadContent(loader);
            Boulder.LoadContent(loader);
            LightBulb.LoadContent(loader);
            LifeCapsule.LoadContent(loader);
            Spider.LoadContent(loader);
            Turret.LoadContent(loader);
            FixedTurret.LoadContent(loader);
            FireCaster.LoadContent(loader);
            FireWizard.LoadContent(loader);
            Cannon.LoadContent(loader);
        }

        public override Color BackgroundColor()
        {
            return Color.Brown;
        }
    }
}
