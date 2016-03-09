using System;

namespace Slip
{
    public abstract class Puzzle
    {
        public virtual void Initialize(Room room, int x, int y)
        {
        }

        public virtual void Update(Room room, int x, int y, Player player)
        {
        }

        public virtual void Draw(GameScreen screen, int x, int y, Main main)
        {
        }

        public virtual bool SolidCollision()
        {
            return false;
        }

        public virtual void OnPlayerCollide(Room room, int x, int y, Player player)
        {
        }

        public virtual bool PlayerInteraction(Room room, int x, int y, Player player)
        {
            return false;
        }
    }
}
