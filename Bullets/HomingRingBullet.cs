using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Slip.Bullets
{
    public class HomingRingBullet : RingBullet
    {
        private float homingSpeed;

        public HomingRingBullet(Vector2 center, float radius, float rotation, int numBullets,
            float bulletSize, Texture2D texture, int timeLeft, float homingSpeed)
            : base(center, radius, rotation, numBullets, bulletSize, texture, timeLeft)
        {
            this.homingSpeed = homingSpeed;
        }

        public override void UpdateState(Room room, Player player)
        {
            if (radius == 0f)
            {
                return;
            }
            float angleOffset = Helper.Vector2ToAngle(player.position - center) - rotation;
            float angleSpace = 2f * (float)Math.PI / numBullets;
            angleOffset %= angleSpace;
            if (angleOffset > angleSpace * 0.5f)
            {
                angleOffset -= angleSpace;
            }
            float adjustment = homingSpeed / radius;
            float angleAdjustment = angleOffset / angleSpace;
            int sign = Math.Sign(angleAdjustment);
            rotationSpeed = adjustment * 0.5f * (sign + angleAdjustment);
            if (Math.Abs(rotationSpeed) > Math.Abs(angleOffset))
            {
                rotationSpeed = angleOffset;
            }
        }
    }
}
