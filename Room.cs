using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Slip
{
    public class Room
    {
        public int floorWidth;
        public int floorHeight;
        public byte[,] floor;
        public byte[,] wall;
        public List<Enemy> enemies;
        public delegate void EnterEvent(Player player);
        public event EnterEvent OnEnter;

        public Room(int width = 50, int height = 50)
        {
            floorWidth = width;
            floorHeight = height;
            floor = new byte[width, height];
            wall = new byte[width, height];
            enemies = new List<Enemy>();
        }

        public void EnterRoom(Player player)
        {
            if (OnEnter != null)
            {
                OnEnter(player);
            }
        }

        public static Vector2 TileToWorldPos(int x, int y)
        {
            return GameScreen.tileSize * new Vector2(x, y);
        }

        public void AddWallBorder(int left, int right, int top, int bottom, byte type = 1)
        {
            for (int x = left; x <= right; x++)
            {
                wall[x, top] = type;
                wall[x, bottom] = type;
            }
            for (int y = top; y <= bottom; y++)
            {
                wall[left, y] = type;
                wall[right, y] = type;
            }
        }
    }
}
