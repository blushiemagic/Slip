using System;

namespace Slip.Puzzles
{
    public class InvisibleSwitch : Puzzle
    {
        public bool pressed = false;
        private Switch.SwitchAction action;

        public InvisibleSwitch(Switch.SwitchAction action)
        {
            this.action = action;
        }

        public override void OnPlayerCollide(Room room, int x, int y, Player player)
        {
            if (!pressed)
            {
                pressed = true;
                action(room, player);
            }
        }
    }
}