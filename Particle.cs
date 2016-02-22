using System;
using Microsoft.Xna.Framework;

namespace Slip
{
    public abstract class Particle
    {
        /** Returns whether this particle should be removed from the room */
        public abstract bool Update(Room room);

        public abstract void Draw(GameScreen screen, Main main);
    }
}
