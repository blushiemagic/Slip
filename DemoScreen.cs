using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Slip
{
    //WARNING: Absolutely horrible programming
    public class DemoScreen : Screen
    {
        private int level;

        private bool dead = false;
        private Vector2 playerPos;
        private Vector2[] turretPos;
        private IList<Vector2> bulletPos = new List<Vector2>();
        private IList<Vector2> bulletSpeed = new List<Vector2>();
        private Vector2[] switchPos;
        private bool[] switchPressed;
        private bool win = false;
        private long time = 0;
        private float rainbow;
        private Vector2 circleCenter;
        private float circleAngle;
        private float circleRadius;
        private int numCircles;

        public DemoScreen(int level = 2)
        {
            this.level = level;
        }

        public override void Initialize(Main main)
        {
            base.Initialize(main);
            Reset();
        }

        public override void UpdateScreen(Main main)
        {
            if (dead)
            {
                Reset();
            }
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
                playerPos += velocity;
            }
            foreach (Vector2 pos in turretPos)
            {
                if (Vector2.Distance(playerPos, pos) <= 20f)
                {
                    dead = true;
                }
                if (!win && time > 0 && (time % (level > 2 ? 40 : 60) == 0))
                {
                    bulletPos.Add(pos);
                    Vector2 speed = playerPos - pos;
                    if (speed == Vector2.Zero)
                    {
                        speed = new Vector2(0f, 1f);
                    }
                    else
                    {
                        speed.Normalize();
                    }
                    speed *= 4f;
                    bulletSpeed.Add(speed);
                    if (bulletPos.Count > 60)
                    {
                        bulletPos.RemoveAt(0);
                        bulletSpeed.RemoveAt(0);
                    }
                }
            }
            for (int k = 0; k < bulletPos.Count; k++)
            {
                bulletPos[k] += bulletSpeed[k];
                if (Vector2.Distance(playerPos, bulletPos[k]) <= 15f)
                {
                    dead = true;
                }
            }
            for (int k = 0; k < switchPos.Length; k++)
            {
                if (Vector2.Distance(playerPos, switchPos[k]) <= 20f)
                {
                    switchPressed[k] = true;
                    win = true;
                    foreach (bool pressed in switchPressed)
                    {
                        if (!pressed)
                        {
                            win = false;
                            break;
                        }
                    }
                    if (win)
                    {
                        bulletPos.Clear();
                        bulletSpeed.Clear();
                    }
                }
            }
            if (level > 1 && !win)
            {
                circleRadius -= 1f;
                if (circleRadius < 0f)
                {
                    circleAngle = 0f;
                    circleRadius = 150f;
                    circleCenter = playerPos;
                }
                circleAngle += 0.03f;
                for (int k = 0; k < numCircles; k++)
                {
                    float angle = circleAngle + k * 2f * (float)Math.PI / numCircles;
                    Vector2 pos = circleCenter + circleRadius * new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle));
                    if (Vector2.Distance(playerPos, pos) <= 15f)
                    {
                        dead = true;
                    }
                }
            }
            rainbow += 0.001f;
            rainbow %= 1f;
            time++;
        }

        private void Reset()
        {
            dead = false;
            time = 0;
            playerPos = Vector2.Zero;
            turretPos = new Vector2[] { new Vector2(-250f, -250f), new Vector2(250f, -250f), new Vector2(-250f, 250f), new Vector2(250f, 250f) };
            bulletPos.Clear();
            bulletSpeed.Clear();
            switchPos = new Vector2[] { new Vector2(0f, -400f), new Vector2(-400f, 0f), new Vector2(400f, 0f), new Vector2(0f, 400f) };
            switchPressed = new bool[] { false, false, false, false };
            win = false;
            if (level > 1)
            {
                circleCenter = Vector2.Zero;
                circleAngle = 0f;
                circleRadius = 150f;
                numCircles = 5;
                if (level > 2)
                {
                    numCircles++;
                }
            }
        }

        public override void DrawScreen(Main main)
        {
            main.spriteBatch.Draw(playerTexture, main.Center(), null, Color.White, playerTexture.Center());
            foreach (Vector2 pos in turretPos)
            {
                main.spriteBatch.Draw(turretTexture, DrawPos(main, pos), null, Color.White, turretTexture.Center());
            }
            foreach (Vector2 pos in bulletPos)
            {
                main.spriteBatch.Draw(bulletTexture, DrawPos(main, pos), null, Color.White, bulletTexture.Center());
            }
            for (int k = 0; k < switchPressed.Length; k++)
            {
                Texture2D texture = switchPressed[k] ? switchPressedTexture : switchTexture;
                main.spriteBatch.Draw(texture, DrawPos(main, switchPos[k]), null, Color.White, texture.Center());
            }
            if (level > 1 && !win)
            {
                for (int k = 0; k < numCircles; k++)
                {
                    float angle = circleAngle + k * 2f * (float)Math.PI / numCircles;
                    Vector2 pos = circleCenter + circleRadius * new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle));
                    main.spriteBatch.Draw(bulletTexture, DrawPos(main, pos), null, Color.White, bulletTexture.Center());
                }
            }
        }

        public Vector2 DrawPos(Main main, Vector2 pos)
        {
            return size / 2f + pos - playerPos;
        }

        public override Color BackgroundColor()
        {
            return win ? Helper.FromHSB(rainbow, 1f, 1f) : Color.Black;
        }

        private Texture2D playerTexture;
        private Texture2D turretTexture;
        private Texture2D bulletTexture;
        private Texture2D switchTexture;
        private Texture2D switchPressedTexture;

        public override void LoadContent(ContentManager loader)
        {
            playerTexture = loader.Load<Texture2D>("Demo/Player");
            turretTexture = loader.Load<Texture2D>("Demo/Turret");
            bulletTexture = loader.Load<Texture2D>("Demo/Bullet");
            switchTexture = loader.Load<Texture2D>("Demo/Switch");
            switchPressedTexture = loader.Load<Texture2D>("Demo/SwitchPressed");
        }
    }
}
