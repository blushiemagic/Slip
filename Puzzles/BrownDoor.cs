using System;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Slip.Puzzles
{
    public class BrownDoor : Door
    {
        public static Texture2D closedTexture;
        public static Texture2D openVerticalTexture;
        public static Texture2D openHorizontalTexture;

        public BrownDoor(bool vertical, bool open = false) : base(vertical, open)
        {
            base.closedTexture = BrownDoor.closedTexture;
            base.openVerticalTexture = BrownDoor.openVerticalTexture;
            base.openHorizontalTexture = BrownDoor.openHorizontalTexture;
        }

        public override bool PlayerInteraction(Room room, int x, int y, Player player)
        {
            if (!open)
            {
                Open();
                return true;
            }
            return false;
        }

        public static void LoadContent(ContentManager loader)
        {
            closedTexture = loader.Load<Texture2D>("BrownDoorClosed");
            openVerticalTexture = loader.Load<Texture2D>("BrownDoorOpenVertical");
            openHorizontalTexture = loader.Load<Texture2D>("BrownDoorOpenHorizontal");
        }
    }
}
