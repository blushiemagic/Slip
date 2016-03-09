using System;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Slip.Puzzles
{
    public class BlueDoor : Door
    {
        public static Texture2D closedTexture;
        public static Texture2D openVerticalTexture;
        public static Texture2D openHorizontalTexture;

        public BlueDoor(bool vertical, bool open = false) : base(vertical, open)
        {
            base.closedTexture = BlueDoor.closedTexture;
            base.openVerticalTexture = BlueDoor.openVerticalTexture;
            base.openHorizontalTexture = BlueDoor.openHorizontalTexture;
        }

        public static void LoadContent(ContentManager loader)
        {
            closedTexture = loader.Load<Texture2D>("BlueDoorClosed");
            openVerticalTexture = loader.Load<Texture2D>("BlueDoorOpenVertical");
            openHorizontalTexture = loader.Load<Texture2D>("BlueDoorOpenHorizontal");
        }
    }
}
