using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Slip
{
    public class CameraChainEvent : CameraEvent
    {
        private List<CameraEvent> events;
        private int currentEvent = 0;

        public CameraChainEvent(List<CameraEvent> events) : base(Vector2.Zero, null)
        {
            this.events = events;
        }

        public override bool Update(Room room, Player player, ref Vector2 camera)
        {
            CameraEvent whichEvent = events[currentEvent];
            if (currentEvent < events.Count - 1 && whichEvent.timer >= whichEvent.endAction)
            {
                currentEvent++;
            }
            return events[currentEvent].Update(room, player, ref camera);
        }
    }
}
