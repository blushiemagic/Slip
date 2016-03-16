using System;

namespace Slip.Puzzles
{
    public class InvisibleSwitch : Puzzle
    {
        public bool pressed = false;
        public delegate void SwitchAction(Room room, Player player);
        private SwitchAction action;

        public InvisibleSwitch(SwitchAction action)
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