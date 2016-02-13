namespace Slip
{
    // This is the general class for turrets. All other turrets are descendants of this class. 
    public class Turret
    {
		// All turrets, no matter what kind, have some kind of texture, a position, and a "dead" bool (they can be active or nonactive)
        public static Texture2D texture;
        public Vector2 position;
        public bool dead;

        public void Draw(Main main)
        {
            main.spriteBatch.Draw(texture, main.Center(), null, Color.White, texture.Center()); //I copied this from Player - Ben
        }

    }
}