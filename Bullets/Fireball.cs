using System;
using Microsoft.Xna.Framework;

namespace Slip.Bullets
{
    public class Fireball : CannonBall
    {
        private float time = 0f;

        public Fireball(Enemy owner, Vector2 position, Vector2 velocity) : base(owner, position, velocity) { }

        public override void UpdateVelocity(Room room, Player player)
        {
            float angle = Helper.Vector2ToAngle(velocity);
            float angleToPlayer = Helper.Vector2ToAngle(player.position - position);
            if (angleToPlayer - angle > MathHelper.Pi)
            {
                angleToPlayer -= MathHelper.TwoPi;
            }
            if (angle - angleToPlayer > MathHelper.Pi)
            {
                angle -= MathHelper.TwoPi;
            }
            float angleOffset = angleToPlayer - angle;
            float speed = 2.5f + 1.5f * (float)Math.Cos(time / 20f);
            velocity = speed * Helper.AngleToVector2(angle + 0.05f * angleOffset);
            time += 1f;
        }
    }
}
