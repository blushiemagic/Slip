using System;

namespace Slip
{
    public class Tile
    {
        public const int tileSize = 40;

        private byte data;
        private const byte floorBits = 1 | 2 | 4;
        private const byte floorBitOffset = 0;
        private const byte floorSize = 1 << 3;
        private const byte wallBits = 8 | 16 | 32;
        private const byte wallBitOffset = 3;
        private const byte wallSize = 1 << 3;
        private const byte checkpointBit = 64;
        private const byte checkpointBitOffset = 6;

        public byte Floor
        {
            get
            {
                return (byte)((data & floorBits) >> floorBitOffset);
            }
            set
            {
                if (value >= floorSize)
                {
                    throw new ArgumentOutOfRangeException("Floor must be <= " + floorSize);
                }
                data = (byte)((data & ~floorBits) | (value << floorBitOffset));
            }
        }

        public byte Wall
        {
            get
            {
                return (byte)((data & wallBits) >> 3);
            }
            set
            {
                if (value >= wallSize)
                {
                    throw new ArgumentOutOfRangeException("Wall must be <= " + wallSize);
                }
                data = (byte)((data & ~wallBits) | (value << wallBitOffset));
            }
        }

        public bool Checkpoint
        {
            get
            {
                return (data & checkpointBit) == checkpointBit;
            }
            set
            {
                if (value)
                {
                    data = (byte)(data | checkpointBit);
                }
                else
                {
                    data = (byte)(data & ~checkpointBit);
                }
            }
        }
    }
}
