using System;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Slip.Puzzles
{
    public class SilverDoor : Door
    {
        public static Texture2D closedTexture;
        public static Texture2D openVerticalTexture;
        public static Texture2D openHorizontalTexture;

        public SilverDoor(bool vertical, bool open = false) : base(vertical, open)
        {
            base.closedTexture = SilverDoor.closedTexture;
            base.openVerticalTexture = SilverDoor.openVerticalTexture;
            base.openHorizontalTexture = SilverDoor.openHorizontalTexture;
        }

        public override bool PlayerInteraction(Room room, int x, int y, Player player)
        {
            if (!open && player.silverKeys > 0)
            {
                Open();
                player.silverKeys--;
                return true;
            }
            return false;
        }

        public static void LoadContent(ContentManager loader)
        {
            closedTexture = loader.Load<Texture2D>("SilverDoorClosed");
            openVerticalTexture = loader.Load<Texture2D>("SilverDoorOpenVertical");
            openHorizontalTexture = loader.Load<Texture2D>("SilverDoorOpenHorizontal");
        }
    }
}
