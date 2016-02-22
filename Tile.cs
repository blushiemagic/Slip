using System;

namespace Slip
{
    public class Tile
    {
        public const int tileSize = 40;

        private byte data;
        private const byte floorBits = 1 | 2 | 4 | 8;
        private const byte floorBitOffset = 0;
        public const byte numFloors = 1 << 4;
        private const byte wallBits = 16 | 32 | 64 | 128;
        private const byte wallBitOffset = 4;
        public const byte numWalls = 1 << 4;

        internal Puzzle puzzle;

        public byte Floor
        {
            get
            {
                return (byte)((data & floorBits) >> floorBitOffset);
            }
            set
            {
                if (value >= numFloors)
                {
                    throw new ArgumentOutOfRangeException("Floor must be <= " + numFloors);
                }
                data = (byte)((data & ~floorBits) | (value << floorBitOffset));
            }
        }

        public byte Wall
        {
            get
            {
                return (byte)((data & wallBits) >> wallBitOffset);
            }
            set
            {
                if (value >= numWalls)
                {
                    throw new ArgumentOutOfRangeException("Wall must be <= " + numWalls);
                }
                data = (byte)((data & ~wallBits) | (value << wallBitOffset));
            }
        }
    }
}
