using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Slip.Puzzles;

namespace Slip
{
    public class Room
    {
        public int width;
        public int height;
        public Tile[,] tiles;
        public List<Enemy> enemies;
        public ObjectList<Bullet> bullets;
        public List<Point> puzzleCache;
        public ObjectList<Particle> particles;
        public delegate void EnterEvent(Player player);
        public event EnterEvent OnEnter;
        public delegate void ExitEvent(Player player);
        public event ExitEvent OnExit;
        public Portal usePortal;
        public CameraEvent cameraEvent;

        public Room(int width = 50, int height = 50)
        {
            this.width = width;
            this.height = height;
            this.tiles = new Tile[width, height];
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    tiles[x, y] = new Tile();
                }
            }
            enemies = new List<Enemy>();
            bullets = new ObjectList<Bullet>();
            puzzleCache = new List<Point>();
            particles = new ObjectList<Particle>();
        }

        public void EnterRoom(Player player)
        {
            if (OnEnter != null)
            {
                OnEnter(player);
            }
        }

        public void ExitRoom(Player player)
        {
            int k = 0;
            while (k < enemies.Count)
            {
                Enemy enemy = enemies[k];
                if (enemy.temporary)
                {
                    enemies.Remove(enemy);
                    k--;
                }
                else if (enemy.defaultPosition.HasValue)
                {
                    enemy.position = enemy.defaultPosition.Value;
                }
                k++;
            }
            bullets.Clear();
            particles.Clear();
            usePortal = null;
            if (OnExit != null)
            {
                OnExit(player);
            }
        }

        public static Vector2 TileToWorldPos(int x, int y)
        {
            return GameScreen.tileSize * new Vector2(x, y);
        }

        public static Vector2 TileToWorldPos(Vector2 v)
        {
            return GameScreen.tileSize * v;
        }

        public void AddEnemyWithDefaultPos(Enemy enemy)
        {
            enemies.Add(enemy);
            enemy.defaultPosition = enemy.position;
        }

        public void AddPuzzle(int x, int y, Puzzle puzzle)
        {
            if (tiles[x, y].puzzle == null)
            {
                puzzleCache.Add(new Point(x, y));
            }
            tiles[x, y].puzzle = puzzle;
            puzzle.Initialize(this, x, y);
        }

        public void AddPuzzle(Vector2 v, Puzzle puzzle)
        {
            AddPuzzle((int)v.X / Tile.tileSize, (int)v.Y / Tile.tileSize, puzzle);
        }

        public void RemovePuzzle(int x, int y)
        {
            if (tiles[x, y].puzzle != null)
            {
                tiles[x, y].puzzle = null;
                puzzleCache.Remove(new Point(x, y));
            }
        }

        public void SetFloor(int x, int y, byte type)
        {
            tiles[x, y].Floor = type;
        }

        public void SetFloor(Vector2 v, byte type)
        {
            tiles[(int)v.X / Tile.tileSize, (int)v.Y / Tile.tileSize].Floor = type;
        }

        public void SetWall(int x, int y, byte type)
        {
            tiles[x, y].Wall = type;
        }

        public void SetWall(Vector2 v, byte type)
        {
            tiles[(int)v.X / Tile.tileSize, (int)v.Y / Tile.tileSize].Wall = type;
        }

        public void SetupFloorsAndWalls(byte floor = 1, byte wall = 1)
        {
            FillFloor(0, width - 1, 0, height - 1, floor);
            AddWallBorder(0, width - 1, 0, height - 1, wall);
        }

        public void FillFloor(int left, int right, int top, int bottom, byte type = 1)
        {
            for (int x = left; x <= right; x++)
            {
                for (int y = top; y <= bottom; y++)
                {
                    tiles[x, y].Floor = type;
                }
            }
        }

        public void FillWall(int left, int right, int top, int bottom, byte type = 1)
        {
            for (int x = left; x <= right; x++)
            {
                for (int y = top; y <= bottom; y++)
                {
                    tiles[x, y].Wall = type;
                }
            }
        }

        public void AddWallBorder(int left, int right, int top, int bottom, byte type = 1)
        {
            for (int x = left; x <= right; x++)
            {
                tiles[x, top].Wall = type;
                tiles[x, bottom].Wall = type;
            }
            for (int y = top; y <= bottom; y++)
            {
                tiles[left, y].Wall = type;
                tiles[right, y].Wall = type;
            }
        }
    }
}
