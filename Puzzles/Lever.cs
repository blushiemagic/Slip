using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Slip.Puzzles
{
    public class Lever : Puzzle
    {
        public static Dictionary<Direction, Texture2D> textures = new Dictionary<Direction, Texture2D>();
        private Direction direction;
        private Switch.SwitchAction action;

        public Lever(Direction direction, Switch.SwitchAction action)
        {
            this.direction = direction;
            this.action = action;
        }

        public Lever(Switch.SwitchAction action)
        {
            this.direction = Direction.Left;
            this.action = action;
        }

        public override void Draw(GameScreen screen, int x, int y, Main main)
        {
            Texture2D texture;
            if (!textures.TryGetValue(direction, out texture))
            {
                texture = textures[Direction.Left];
            }
            main.spriteBatch.Draw(texture, screen.DrawPos(main, Room.TileToWorldPos(x, y)), null, Color.White);
        }

        public override bool SolidCollision()
        {
            return true;
        }

        public override bool PlayerInteraction(Room room, int x, int y, Player player)
        {
            switch (direction)
            {
                case Direction.Up:
                    direction = Direction.Down;
                    break;
                case Direction.Down:
                    direction = Direction.Up;
                    break;
                case Direction.Left:
                    direction = Direction.Right;
                    break;
                case Direction.Right:
                    direction = Direction.Left;
                    break;
            }
            action(room, player);
            return true;
        }

        public static void LoadContent(ContentManager loader)
        {
            textures[Direction.Up] = loader.Load<Texture2D>("LeverUp");
            textures[Direction.Down] = loader.Load<Texture2D>("LeverDown");
            textures[Direction.Left] = loader.Load<Texture2D>("LeverLeft");
            textures[Direction.Right] = loader.Load<Texture2D>("LeverRight");
        }
    }
}
