using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Slip
{
    public class Text : Component
    {
        private const int textBorder = 2;
        private string text;
        private Color color;
        private Color borderColor;

        public Text(string text, Color color, Color borderColor)
        {
            this.text = text;
            this.color = color;
            this.borderColor = borderColor;
        }

        public override void Initialize(Main main)
        {
            border = new Vector2(textBorder, textBorder);
            size = Textures.Font.MeasureString(text) + 2f * border;
        }

        public override void Draw(Main main)
        {
            main.spriteBatch.DrawBorderString(Textures.Font, text, Position, color, borderColor, textBorder);
        }
    }
}
