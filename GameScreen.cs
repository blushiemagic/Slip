﻿using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Slip.Puzzles;

namespace Slip
{
    public abstract class GameScreen : Screen
    {
        public const int tileSize = Tile.tileSize;
        
        public Player player;
        public Room currentRoom;
        private Vector2 camera;
        private int reviveTimer;
        private const int maxReviveTimer = 120;

        public override void Initialize(Main main)
        {
            base.Initialize(main);
            player = new Player();
            player.position = new Vector2(tileSize * 1.5f, tileSize * 1.5f);
            currentRoom = SetupLevel(player);
            camera = player.position;
        }

        public abstract Room SetupLevel(Player player);

        public override void UpdateScreen(Main main)
        {
            if (player.life <= 0)
            {
                reviveTimer++;
                if (reviveTimer >= maxReviveTimer)
                {
                    player.Revive(this);
                    reviveTimer = 0;
                }
                return;
            }
            if (currentRoom.cameraEvent != null)
            {
                if (currentRoom.cameraEvent.Update(currentRoom, player, ref camera))
                {
                    currentRoom.cameraEvent = null;
                }
                return;
            }
            player.Update(currentRoom);
            foreach (Enemy enemy in currentRoom.enemies)
            {
                enemy.UpdateEnemy(currentRoom, player);
                if (!enemy.IsHurt && enemy.Collides(player))
                {
                    player.TakeDamage(1);
                }
            }
            currentRoom.bullets.Update(currentRoom, player);
            foreach (Point pos in currentRoom.puzzleCache)
            {
                currentRoom.tiles[pos.X, pos.Y].puzzle.Update(currentRoom, pos.X, pos.Y, player);
            }
            currentRoom.particles.Update(currentRoom, player);
            if (currentRoom.usePortal != null)
            {
                ChangeRoom(currentRoom.usePortal.targetRoom, currentRoom.usePortal.targetPos);
            }
            camera = player.position;
        }

        public void ChangeRoom(Room room, Vector2 position)
        {
            if (currentRoom != null)
            {
                currentRoom.ExitRoom(player);
            }
            currentRoom = room;
            player.position = position;
            room.EnterRoom(player);
        }

        public override void DrawScreen(Main main)
        {
            Lava.IncrementTimer();
            int startTileX, endTileX, startTileY, endTileY;
            ScreenBoundTiles(out startTileX, out endTileX, out startTileY, out endTileY);
            for (int x = startTileX; x <= endTileX; x++)
            {
                for (int y = startTileY; y <= endTileY; y++)
                {
                    Texture2D texture = floorTexture[currentRoom.tiles[x, y].Floor];
                    if (texture != null)
                    {
                        Vector2 worldPos = tileSize * new Vector2(x, y);
                        main.spriteBatch.Draw(texture, DrawPos(main, worldPos), null, Color.White, Vector2.Zero);
                    }
                }
            }
            for (int x = startTileX; x <= endTileX; x++)
            {
                for (int y = startTileY; y <= endTileY; y++)
                {
                    Texture2D texture = wallTexture[currentRoom.tiles[x, y].Wall];
                    if (texture != null)
                    {
                        Vector2 worldPos = tileSize * new Vector2(x, y);
                        main.spriteBatch.Draw(texture, DrawPos(main, worldPos), null, Color.White, Vector2.Zero);
                    }
                }
            }
            foreach (Point pos in currentRoom.puzzleCache)
            {
                currentRoom.tiles[pos.X, pos.Y].puzzle.Draw(this, pos.X, pos.Y, main);
            }
            foreach (Enemy enemy in currentRoom.enemies)
            {
                enemy.Draw(this, main);
            }
            player.Draw(this, main);
            currentRoom.bullets.Draw(this, main);
            currentRoom.particles.Draw(this, main);
            if (player.life <= 0)
            {
                float alpha = 0.1f + 0.4f * ((float)reviveTimer / (float)maxReviveTimer);
                main.spriteBatch.Draw(Textures.Pixel, Vector2.Zero, null, Color.Black * alpha, 0f, Vector2.Zero,
                    size, SpriteEffects.None, 0f);
            }
            DrawHUD(main);
        }

        private void DrawHUD(Main main)
        {
            string text = "LVL1  HP:" + player.life + "/" + player.maxLife + "  EXP:" + player.exp + "/9999";
            Vector2 offset = new Vector2(10f, 10f);
            Vector2 textSize = Textures.Font.MeasureString(text);
            main.spriteBatch.DrawBorderString(Textures.Font, text, offset, Color.White, Color.Black, 2);
            offset.X += textSize.X + 40f;
            main.spriteBatch.Draw(SilverKey.texture, offset - new Vector2(0f, 5f), null, Color.White,
                0f, Vector2.Zero, 2f, SpriteEffects.None, 0f);
            offset.X += 30f;
            text = "X" + player.silverKeys;
            main.spriteBatch.DrawBorderString(Textures.Font, text, offset, Color.White, Color.Black, 2);
            if (player.life <= 0)
            {
                text = "YOU HAVE DIED!";
                main.spriteBatch.DrawCenteredBorderString(Textures.Font, text, size / 2f + new Vector2(0f, 50f),
                    Color.White, Color.Black, 2);
            }
        }

        public bool BoxOnScreen(Hitbox box)
        {
            if (box.topLeft.X > size.X / 2f + camera.X)
            {
                return false;
            }
            if (box.topRight.X < -size.X / 2f + camera.X)
            {
                return false;
            }
            if (box.topLeft.Y > size.Y / 2f + camera.Y)
            {
                return false;
            }
            if (box.bottomLeft.Y * tileSize < -size.Y / 2f + camera.Y)
            {
                return false;
            }
            return true;
        }

        public void ScreenBoundTiles(out int left, out int right, out int top, out int bottom)
        {
            left = (int)Math.Floor((-size.X / 2f + camera.X) / tileSize);
            if (left < 0)
            {
                left = 0;
            }
            right = (int)Math.Floor((size.X / 2f + camera.X) / tileSize);
            if (right >= currentRoom.width)
            {
                right = currentRoom.width - 1;
            }
            top = (int)Math.Floor((-size.Y / 2f + camera.Y) / tileSize);
            if (top < 0)
            {
                top = 0;
            }
            bottom = (int)Math.Floor((size.Y / 2f + camera.Y) / tileSize);
            if (bottom >= currentRoom.height)
            {
                bottom = currentRoom.height - 1;
            }
        }

        public Vector2 DrawPos(Main main, Vector2 pos)
        {
            return size / 2f + pos - camera;
        }

        protected Texture2D[] floorTexture = new Texture2D[Tile.numFloors];
        protected Texture2D[] wallTexture = new Texture2D[Tile.numWalls];

        public override void LoadContent(ContentManager loader)
        {
            base.LoadContent(loader);
            Player.LoadContent(loader);
            Checkpoint.LoadContent(loader);
            SilverKey.LoadContent(loader);
        }

        protected void LoadTileset(string folder, int floors, int walls, ContentManager loader)
        {
            for (int k = 1; k <= floors; k++)
            {
                floorTexture[k] = loader.Load<Texture2D>(folder + "/Floor" + k);
            }
            for (int k = 1; k <= walls; k++)
            {
                wallTexture[k] = loader.Load<Texture2D>(folder + "/Wall" + k);
            }
        }
    }
}
