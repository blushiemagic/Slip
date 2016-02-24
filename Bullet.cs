using System;

namespace Slip
{
    public abstract class Bullet : ListObject
    {
        public override sealed bool Update(Room room, Player player)
        {
            if (UpdateBullet(room, player))
            {
                return true;
            }
            if (Collides(player))
            {
                player.TakeDamage(1);
            }
            return false;
        }

        public abstract bool UpdateBullet(Room room, Player player);

        public abstract bool Collides(Player player);
    }
}
