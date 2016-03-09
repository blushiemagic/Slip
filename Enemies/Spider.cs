using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Slip.Enemies
{
    public class Spider : Enemy
    {
        public static Texture2D texture;
        private const float viewWidth = 10 * Tile.tileSize;
        private const float viewHeight = 7 * Tile.tileSize;

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

        public Spider(Vector2 pos) : base(pos, 20f) { }

        public override void Update(Room room, Player player)
        {
            if (Math.Abs(player.position.X - position.X) <= viewWidth &&
                Math.Abs(player.position.Y - position.Y) <= viewHeight)
            {
                Vector2 offset = player.position - position;
                if (offset != Vector2.Zero)
                {
                    offset.Normalize();
                    offset *= 2f;
                    bool collided;
                    TopLeft = Collision.MovePos(TopLeft, size, size, offset, room, out collided);
                }
            }
        }

        public override void Draw(GameScreen screen, Main main)
        {
            main.spriteBatch.Draw(texture, screen.DrawPos(main, position), null, Color.White, texture.Center());
        }

        public static void LoadContent(ContentManager loader)
        {
            texture = loader.Load<Texture2D>("Spider");
        }
    }
}
