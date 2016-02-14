using System;
using System.Collections.Generic;

namespace Slip
{
    public class Room
    {
        public int floorWidth;
        public int floorHeight;
        public byte[,] floor;
        public delegate void EnterEvent(Player player);
        public event EnterEvent OnEnter;

        public Room(int width = 50, int height = 50)
        {
            floorWidth = width;
            floorHeight = height;
            floor = new byte[width, height];
        }

        public void EnterRoom(Player player)
        {
            if (OnEnter != null)
            {
                OnEnter(player);
            }
        }
    }
}
