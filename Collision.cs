using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Slip
{
    public static class Collision
    {
        public const int tileSize = Tile.tileSize;

        public static Vector2 MovePos(Vector2 position, float width, float height, Vector2 velocity, Room room)
        {
            if (velocity == Vector2.Zero)
            {
                return position;
            }
            while (velocity != Vector2.Zero)
            {
                Hitbox box = new Hitbox(position, width, height);
                CollideLine? collision = GetNextCollision(box, velocity, room);
                float distance = collision.HasValue ? collision.Value.distance : velocity.Length();
                Vector2 move = velocity;
                move.Normalize();
                move *= distance;
                position += move;
                velocity -= move;
                if (collision.HasValue)
                {
                    switch (collision.Value.type)
                    {
                        case CollideType.X:
                            velocity.X = 0f;
                            break;
                        case CollideType.Y:
                            velocity.Y = 0f;
                            break;
                    }
                }
            }
            return position;
        }

        private static CollideLine? GetNextCollision(Hitbox box, Vector2 velocity, Room room)
        {
            CollideLine? xLine = NextXCollision(box, velocity, room);
            CollideLine? yLine = NextYCollision(box, velocity, room);
            if (!xLine.HasValue)
            {
                return yLine;
            }
            if (!yLine.HasValue)
            {
                return xLine;
            }
            return xLine.Value.distance < yLine.Value.distance ? xLine : yLine;
        }

        private static CollideLine? NextXCollision(Hitbox box, Vector2 velocity, Room room)
        {
            if (velocity.X > 0f)
            {
                int x = (int)Math.Ceiling(box.topRight.X / tileSize);
                int end = (int)Math.Floor((box.topRight.X + velocity.X) / tileSize);
                while (x <= end)
                {
                    float xDistance = tileSize * x - box.topRight.X;
                    float yDistance = xDistance / velocity.X * velocity.Y;
                    int y = (int)Math.Floor((box.topRight.Y + yDistance) / tileSize);
                    int yEnd = (int)Math.Ceiling((box.bottomRight.Y + yDistance) / tileSize) - 1;
                    while (y <= yEnd)
                    {
                        if (room.tiles[x, y].Wall > 0)
                        {
                            return new CollideLine
                            {
                                type = CollideType.X,
                                position = x * tileSize,
                                start = y * tileSize,
                                end = (y + 1) * tileSize,
                                distance = (float)Math.Sqrt(xDistance * xDistance + yDistance * yDistance)
                            };
                        }
                        y++;
                    }
                    x++;
                }
            }
            else if (velocity.X < 0f)
            {
                int x = (int)Math.Floor(box.topLeft.X / tileSize);
                int end = (int)Math.Ceiling((box.topLeft.X + velocity.X) / tileSize);
                while (x >= end)
                {
                    float xDistance = tileSize * x - box.topLeft.X;
                    float yDistance = xDistance / velocity.X * velocity.Y;
                    int y = (int)Math.Floor((box.topLeft.Y + yDistance) / tileSize);
                    int yEnd = (int)Math.Ceiling((box.bottomLeft.Y + yDistance) / tileSize) - 1;
                    while (y <= yEnd)
                    {
                        if (room.tiles[x - 1, y].Wall > 0)
                        {
                            return new CollideLine
                            {
                                type = CollideType.X,
                                position = x * tileSize,
                                start = y * tileSize,
                                end = (y + 1) * tileSize,
                                distance = (float)Math.Sqrt(xDistance * xDistance + yDistance * yDistance)
                            };
                        }
                        y++;
                    }
                    x--;
                }
            }
            return null;
        }

        private static CollideLine? NextYCollision(Hitbox box, Vector2 velocity, Room room)
        {
            if (velocity.Y > 0f)
            {
                int y = (int)Math.Ceiling(box.bottomLeft.Y / tileSize);
                int end = (int)Math.Floor((box.bottomLeft.Y + velocity.Y) / tileSize);
                while (y <= end)
                {
                    float yDistance = tileSize * y - box.bottomLeft.Y;
                    float xDistance = yDistance / velocity.Y * velocity.X;
                    int x = (int)Math.Floor((box.bottomLeft.X + xDistance) / tileSize);
                    int xEnd = (int)Math.Ceiling((box.bottomRight.X + xDistance) / tileSize) - 1;
                    while (x <= xEnd)
                    {
                        if (room.tiles[x, y].Wall > 0)
                        {
                            return new CollideLine
                            {
                                type = CollideType.Y,
                                position = y * tileSize,
                                start = x * tileSize,
                                end = (x + 1) * tileSize,
                                distance = (float)Math.Sqrt(xDistance * xDistance + yDistance * yDistance)
                            };
                        }
                        x++;
                    }
                    y++;
                }
            }
            else if (velocity.Y < 0f)
            {
                int y = (int)Math.Floor(box.topLeft.Y / tileSize);
                int end = (int)Math.Ceiling((box.topLeft.Y + velocity.Y) / tileSize);
                while (y >= end)
                {
                    float yDistance = tileSize * y - box.topLeft.Y;
                    float xDistance = yDistance / velocity.Y * velocity.X;
                    int x = (int)Math.Floor((box.bottomLeft.X + xDistance) / tileSize);
                    int xEnd = (int)Math.Ceiling((box.bottomRight.X + xDistance) / tileSize) - 1;
                    while (x <= xEnd)
                    {
                        if (room.tiles[x, y - 1].Wall > 0)
                        {
                            return new CollideLine
                            {
                                type = CollideType.Y,
                                position = y * tileSize,
                                start = x * tileSize,
                                end = (x + 1) * tileSize,
                                distance = (float)Math.Sqrt(xDistance * xDistance + yDistance * yDistance)
                            };
                        }
                        x++;
                    }
                    y--;
                }
            }
            return null;
        }
    }

    enum CollideType
    {
        X,
        Y
    }

    struct CollideLine
    {
        public CollideType type;
        public float position;
        public float start;
        public float end;
        public float distance;
    }

    public struct Hitbox
    {
        public Vector2 topLeft;
        public Vector2 topRight;
        public Vector2 bottomLeft;
        public Vector2 bottomRight;
        public float width;
        public float height;

        public Hitbox(Vector2 position, float width, float height)
        {
            this.topLeft = position;
            this.topRight = position;
            this.topRight.X += width;
            this.bottomLeft = position;
            this.bottomLeft.Y += height;
            this.bottomRight = this.bottomLeft;
            this.bottomRight.X += width;
            this.width = width;
            this.height = height;
        }
    }
}
