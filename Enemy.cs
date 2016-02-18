using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Slip
{
    public abstract class Enemy
    {
        public Vector2 position;

        public Enemy(Vector2 pos)
        {
            this.position = pos;
        }

        public abstract void Update(Room room);

        public abstract void Draw(GameScreen screen, Main main);
    }
}
