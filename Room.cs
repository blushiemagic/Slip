﻿using System;
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

        public void AddPuzzle(int x, int y, Puzzle puzzle)
        {
            if (tiles[x, y].puzzle == null)
            {
                puzzleCache.Add(new Point(x, y));
            }
            tiles[x, y].puzzle = puzzle;
            puzzle.Initialize(this, x, y);
        }

        public void RemovePuzzle(int x, int y)
        {
            if (tiles[x, y].puzzle != null)
            {
                tiles[x, y].puzzle = null;
                puzzleCache.Remove(new Point(x, y));
            }
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
