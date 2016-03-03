using System;
using Microsoft.Xna.Framework;

namespace Slip
{
    public static class Helper
    {
        public static Vector2 PointwiseMult(Vector2 vector1, Vector2 vector2)
        {
            return new Vector2(vector1.X * vector2.X, vector1.Y * vector2.Y);
        }

        public static void GetDirection(Vector2 velocity, ref Direction direction)
        {
            if (velocity == Vector2.Zero)
            {
                return;
            }
            if (velocity.X == 0f)
            {
                direction = velocity.Y > 0f ? Direction.Down : Direction.Up;
            }
            else if (velocity.Y == 0f)
            {
                direction = velocity.X > 0f ? Direction.Right : Direction.Left;
            }
            else if (velocity.X > 0f)
            {
                direction = velocity.Y > 0f ? Direction.DownRight : Direction.UpRight;
            }
            else
            {
                direction = velocity.Y > 0f ? Direction.DownLeft : Direction.UpLeft;
            }
        }

        public static Color FromHSB(float h, float s, float b)
        {
            Color color = Color.White;
            float section = (h - (int)h) * 6f;
            float f = section - (int)section;
            float p = b * (1f - s);
            float q = b * (1f - s * f);
            float t = b * (1f - s * (1f - f));
            switch ((int)section)
            {
                case 0:
                    color.R = (byte)(b * 255f + 0.5f);
                    color.G = (byte)(t * 255f + 0.5f);
                    color.B = (byte)(p * 255f + 0.5f);
                    break;
                case 1:
                    color.R = (byte)(q * 255f + 0.5f);
                    color.G = (byte)(b * 255f + 0.5f);
                    color.B = (byte)(p * 255f + 0.5f);
                    break;
                case 2:
                    color.R = (byte)(p * 255f + 0.5f);
                    color.G = (byte)(b * 255f + 0.5f);
                    color.B = (byte)(t * 255f + 0.5f);
                    break;
                case 3:
                    color.R = (byte)(p * 255f + 0.5f);
                    color.G = (byte)(q * 255f + 0.5f);
                    color.B = (byte)(b * 255f + 0.5f);
                    break;
                case 4:
                    color.R = (byte)(t * 255f + 0.5f);
                    color.G = (byte)(p * 255f + 0.5f);
                    color.B = (byte)(b * 255f + 0.5f);
                    break;
                case 5:
                    color.R = (byte)(b * 255f + 0.5f);
                    color.G = (byte)(p * 255f + 0.5f);
                    color.B = (byte)(q * 255f + 0.5f);
                    break;
            }
            return color;
        }
    }
}
