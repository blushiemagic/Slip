using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Slip.Enemies;
using Slip.Puzzles;

namespace Slip.Bullets
{
    public class CannonBall : Bullet
    {
        public const float size = 40f;
        public static Texture2D texture;

        public Enemy owner;
        public Vector2 position;
        public Vector2 velocity;

        public Vector2 TopLeft
        {
            get
            {
                return position - 0.5f * new Vector2(size, size);
            }
            set
            {
                position = value + 0.5f * new Vector2(size, size);
            }
        }

        public float radius
        {
            get
            {
                return size / 2f;
            }
        }

        public CannonBall(Enemy owner, Vector2 position, Vector2 velocity)
        {
            this.owner = owner;
            this.position = position;
            this.velocity = velocity;
        }

        public override bool UpdateBullet(Room room, Player player)
        {
            bool collided;
            UpdateVelocity(room, player);
            Vector2 topLeft = Collision.MovePos(TopLeft, size, size, velocity, room, out collided);
            TopLeft = topLeft;
            if (collided)
            {
                int left = (int)(topLeft.X / Tile.tileSize);
                int top = (int)(topLeft.Y / Tile.tileSize);
                int right = (int)((topLeft.X + size) / Tile.tileSize);
                int bottom = (int)((topLeft.Y + size) / Tile.tileSize);
                bool flag = false;
                if (topLeft.X % Tile.tileSize == 0f)
                {
                    for (int y = top; y <= bottom; y++)
                    {
                        if (room.tiles[left - 1, y].puzzle is Boulder)
                        {
                            Boulder.Throw(room, left - 1, y, velocity);
                            flag = true;
                        }
                    }
                }
                if ((topLeft.X + size) % Tile.tileSize == 0f)
                {
                    for (int y = top; y <= bottom; y++)
                    {
                        if (room.tiles[right, y].puzzle is Boulder)
                        {
                            Boulder.Throw(room, right, y, velocity);
                            flag = true;
                        }
                    }
                }
                if (topLeft.Y % Tile.tileSize == 0f)
                {
                    for (int x = left; x <= right; x++)
                    {
                        if (room.tiles[x, top - 1].puzzle is Boulder)
                        {
                            Boulder.Throw(room, x, top - 1, velocity);
                            flag = true;
                        }
                    }
                }
                if ((topLeft.Y + size) % Tile.tileSize == 0f)
                {
                    for (int x = left; x <= right; x++)
                    {
                        if (room.tiles[x, bottom].puzzle is Boulder)
                        {
                            Boulder.Throw(room, x, bottom, velocity);
                            flag = true;
                        }
                    }
                }
                if (owner is Cannon && flag)
                {
                    float magnitude = ((Cannon)owner).velocity.Length();
                    ((Cannon)owner).velocity *= 2f;
                }
                return true;
            }
            return false;
        }

        public virtual void UpdateVelocity(Room room, Player player) { }

        public override void Draw(GameScreen screen, Main main)
        {
            main.spriteBatch.Draw(texture, screen.DrawPos(main, position), null, Color.White,
                Helper.Vector2ToAngle(velocity), texture.Center(), 1f, SpriteEffects.None, 1f);
        }

        public override bool Collides(Player player)
        {
            return Vector2.Distance(player.position, position) < player.radius + radius;
        }

        public static void LoadContent(ContentManager loader)
        {
            texture = loader.Load<Texture2D>("CannonBall");
        }
    }
}
