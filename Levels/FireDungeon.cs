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
        Room topLeft = new Room(22, 22);

        public override Room SetupLevel(Player player)
        {
            Vector2 bottomStart = Room.TileToWorldPos(new Vector2(bottom.width * 0.5f, bottom.height - 2.5f));
            Vector2 bottomEnd = Room.TileToWorldPos(new Vector2(bottom.width * 0.5f, 3.5f));
            Vector2 centerBottom = Room.TileToWorldPos(new Vector2(center.width * 0.5f, center.height - 2.5f));
            Vector2 centerTopLeft = Room.TileToWorldPos(new Vector2(2.5f, 2.5f));
            Vector2 topLeftStart = Room.TileToWorldPos(new Vector2(topLeft.width - 2.5f, topLeft.height - 2.5f));

            player.position = new Vector2(60f, 60f);
            start.SetupFloorsAndWalls();
            start.AddPuzzle(1, 1, new Portal(topLeft, topLeftStart));

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
            center.FillWall(9, 9, 1, 8);
            center.FillWall(11, 11, 1, 8);
            for (int k = 2; k <= 8; k += 2)
            {
                center.AddPuzzle(center.width / 2, k, new SilverDoor(true));
            }

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

            return start;
        }

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

        public override void LoadContent(ContentManager loader)
        {
            base.LoadContent(loader);
            LoadTileset("FireDungeon", 2, 1, loader);
            BrownDoor.LoadContent(loader);
            BlueDoor.LoadContent(loader);
            SilverDoor.LoadContent(loader);
            Switch.LoadContent(loader);
            Lava.LoadContent(loader);
            LifeCapsule.LoadContent(loader);
            FixedTurret.LoadContent(loader);
        }

        public override Color BackgroundColor()
        {
            return Color.Brown;
        }
    }
}
