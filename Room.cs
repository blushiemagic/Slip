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
        public delegate void EnterEvent(Room room, Player player);
        public event EnterEvent OnEnter;
        public delegate void ExitEvent(Room room, Player player);
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
                OnEnter(this, player);
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
                OnExit(this, player);
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

    public class MonsterRoom
    {
        public delegate List<CameraEvent> CreateStartEvent(Room room, Player player);
        private CreateStartEvent CreateStart;
        private Vector2 spawnEnemyCamera;
        public delegate void CreateEnemiesEvent(Room room, Player player, List<Enemy> enemies);
        private CreateEnemiesEvent CreateEnemies;
        private List<Enemy> enemies;
        public delegate List<CameraEvent> CreateEndEvent(Room room, Player player);
        private CreateEndEvent CreateEnd;
        public bool cleared;

        public MonsterRoom(Room room, CreateStartEvent createStart, Vector2 spawnEnemyCamera,
            CreateEnemiesEvent createEnemies, CreateEndEvent createEnd)
        {
            this.CreateStart = createStart;
            this.spawnEnemyCamera = spawnEnemyCamera;
            this.CreateEnemies = createEnemies;
            this.enemies = new List<Enemy>();
            this.CreateEnd = createEnd;
            this.cleared = false;
            room.OnExit += (Room r, Player player) => this.enemies.Clear();
        }

        public void DoAction(Room room, Player player)
        {
            List<CameraEvent> startEvents = CreateStart(room, player);
            List<CameraEvent> endEvents = CreateEnd(room, player);
            Enemy.DeathAction onDeath = (Enemy e, Room r, Player p) =>
            {
                enemies.Remove(e);
                if (enemies.Count == 0)
                {
                    r.cameraEvent = new CameraChainEvent(endEvents);
                    cleared = true;
                }
            };
            startEvents.Add(new CameraEvent(spawnEnemyCamera, (Room r, Player p, int time) =>
            {
                CreateEnemies(r, p, enemies);
                foreach (Enemy e in enemies)
                {
                    e.temporary = true;
                    e.OnDeath += onDeath;
                    r.enemies.Add(e);
                }
            }));
            room.cameraEvent = new CameraChainEvent(startEvents);
        }
    }
}
