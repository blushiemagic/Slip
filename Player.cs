using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Slip.Puzzles;

namespace Slip
{
    public class Player
    {
        public static Texture2D[] textures = new Texture2D[(int)Direction.Count];
        public static Texture2D slashTexture;
        public const int size = 20;
        public const int attackDistance = 20;
        public Vector2 position;
        public Direction direction = Direction.Down;
        private int attackTimer = 0;
        public int life = 1;
        public int maxLife = 1;
        public int exp = 0;
        public int invincible = 0;
        public bool debug = false;
        public Checkpoint lastCheckpoint;
        public int silverKeys = 0;

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

        public Hitbox hitbox
        {
            get
            {
                return new Hitbox(TopLeft, size, size);
            }
        }

        public float radius
        {
            get
            {
                return size / 2f;
            }
        }

        public bool Attacking
        {
            get
            {
                return attackTimer > 0;
            }
        }

        public bool AttackCanDamage
        {
            get
            {
                return attackTimer > 5;
            }
        }

        public void Update(Room room)
        {
            if (Main.IsControlPressed(KeyControl.Debug))
            {
                debug = !debug;
            }
            if (Attacking)
            {
                attackTimer--;
            }
            if (!Attacking && Main.IsControlPressed(KeyControl.Action))
            {
                Action(room);
            }
            if (Attacking)
            {
                Attack(room);
            }
            else
            {
                Move(room);
            }
            Hitbox box = hitbox;
            int left = (int)Math.Floor(box.topLeft.X / Tile.tileSize);
            int right = (int)Math.Ceiling(box.topRight.X / Tile.tileSize);
            int top = (int)Math.Floor(box.topLeft.Y / Tile.tileSize);
            int bottom = (int)Math.Ceiling(box.bottomLeft.Y / Tile.tileSize);
            for (int x = left; x < right; x++)
            {
                for (int y = top; y < bottom; y++)
                {
                    Puzzle puzzle = room.tiles[x, y].puzzle;
                    if (puzzle != null)
                    {
                        puzzle.OnPlayerCollide(room, x, y, this);
                    }
                }
            }
            if (invincible > 0)
            {
                invincible--;
            }
        }

        public void Move(Room room)
        {
            Vector2 velocity = Vector2.Zero;
            if (Main.IsControlHeld(KeyControl.Up))
            {
                velocity.Y -= 1f;
            }
            if (Main.IsControlHeld(KeyControl.Down))
            {
                velocity.Y += 1f;
            }
            if (Main.IsControlHeld(KeyControl.Left))
            {
                velocity.X -= 1f;
            }
            if (Main.IsControlHeld(KeyControl.Right))
            {
                velocity.X += 1f;
            }
            if (velocity != Vector2.Zero)
            {
                velocity.Normalize();
                velocity *= 4f;
                Helper.GetDirection(velocity, ref direction);
                bool collided;
                TopLeft = Collision.MovePos(TopLeft, size, size, velocity, room, out collided);
            }
        }

        public void Action(Room room)
        {
            Vector2 checkDirection = Helper.DirectionToVector2(direction);
            float checkPosOffset = size;
            if (checkDirection.X != 0f && checkDirection.Y != 0f)
            {
                checkPosOffset *= (float)Math.Sqrt(2);
            }
            Vector2 checkPos = position + checkPosOffset * checkDirection;
            checkPos /= Tile.tileSize;
            if (checkDirection.X > 0f && checkPos.X == (float)Math.Floor(checkPos.X))
            {
                checkPos.X -= 1f;
            }
            if (checkDirection.Y > 0f && checkPos.Y == (float)Math.Floor(checkPos.Y))
            {
                checkPos.Y -= 1f;
            }
            int x = (int)checkPos.X;
            int y = (int)checkPos.Y;
            Puzzle puzzle = room.tiles[x, y].puzzle;
            if (puzzle != null && puzzle.PlayerInteraction(room, x, y, this))
            {
                return;
            }
            attackTimer = 15;
        }

        public void Attack(Room room)
        {
            if (!AttackCanDamage)
            {
                return;
            }
            float attackDir = Helper.DirectionToRotation(direction);
            float angle1 = attackDir - 0.25f * (float)Math.PI;
            float angle2 = attackDir + 0.25f * (float)Math.PI;
            float reach = radius + attackDistance;
            int i = 0;
            while (i < room.enemies.Count)
            {
                Enemy enemy = room.enemies[i];
                if (Collision.SectorCollides(position, reach, angle1, angle2, enemy.position, enemy.radius))
                {
                    if (enemy.TakeDamage(1, room, this))
                    {
                        i--;
                    }
                }
                i++;
            }
        }

        public void TakeDamage(int damage)
        {
            if (invincible <= 0 && !debug)
            {
                life -= damage;
                if (life < 0)
                {
                    life = 0;
                }
                invincible = 60;
            }
        }

        public void Revive(GameScreen screen)
        {
            life = maxLife;
            attackTimer = 0;
            screen.ChangeRoom(lastCheckpoint.room, lastCheckpoint.position);
        }

        public void Draw(GameScreen screen, Main main)
        {
            Color color = Color.White;
            if (invincible > 0)
            {
                color *= 0.75f;
            }
            Texture2D texture = textures[(int)direction];
            main.spriteBatch.Draw(texture, screen.DrawPos(main, position), null, color, texture.Center());
            if (Attacking)
            {
                color = Color.White;
                if (!AttackCanDamage)
                {
                    color *= 0.5f;
                }
                float rotation = Helper.DirectionToRotation(direction);
                main.spriteBatch.Draw(slashTexture, screen.DrawPos(main, position), null, color, rotation,
                    slashTexture.Center(), 1f, SpriteEffects.None, 1f);
            }
        }

        public static void LoadContent(ContentManager loader)
        {
            textures[(int)Direction.Down] = loader.Load<Texture2D>("Player/Player_Down");
            textures[(int)Direction.DownLeft] = loader.Load<Texture2D>("Player/Player_DownLeft");
            textures[(int)Direction.Left] = loader.Load<Texture2D>("Player/Player_Left");
            textures[(int)Direction.UpLeft] = loader.Load<Texture2D>("Player/Player_UpLeft");
            textures[(int)Direction.Up] = loader.Load<Texture2D>("Player/Player_Up");
            textures[(int)Direction.UpRight] = loader.Load<Texture2D>("Player/Player_UpRight");
            textures[(int)Direction.Right] = loader.Load<Texture2D>("Player/Player_Right");
            textures[(int)Direction.DownRight] = loader.Load<Texture2D>("Player/Player_DownRight");
            slashTexture = loader.Load<Texture2D>("Slash");
        }
    }
}
