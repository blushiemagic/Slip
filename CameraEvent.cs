using System;
using Microsoft.Xna.Framework;

namespace Slip
{
    public class CameraEvent
    {
        private const float speed = 10f;
        private Vector2 targetPos;
        public delegate void ActionEvent(Room room, Player player, int time);
        public event ActionEvent DoAction;
        private int actionLength;
        private int timer = 0;

        public CameraEvent(Vector2 target, ActionEvent action, int length = 1)
        {
            this.targetPos = target;
            this.DoAction = action;
            this.actionLength = length;
        }

        public bool Update(Room room, Player player, ref Vector2 camera)
        {
            if (timer == 0 && camera != targetPos)
            {
                Vector2 offset = targetPos - camera;
                if (offset.Length() < speed)
                {
                    camera = targetPos;
                }
                else
                {
                    offset.Normalize();
                    offset *= speed;
                    camera += offset;
                }
            }
            else if (timer < 60 + actionLength)
            {
                timer++;
                if (timer >= 30 && timer < 30 + actionLength)
                {
                    DoAction(room, player, timer - 30);
                }
            }
            else
            {
                Vector2 offset = player.position - camera;
                if (offset.Length() < speed)
                {
                    camera = player.position;
                    return true;
                }
                offset.Normalize();
                offset *= speed;
                camera += offset;
            }
            return false;
        }
    }
}
