using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Slip
{
    public class TextPanel : Panel
    {
        public string Text
        {
            get;
            set;
        }

        public Color TextColor
        {
            get;
            set;
        }

        public TextPanel(string text, Color textColor)
        {
            this.Text = text;
            this.TextColor = textColor;
        }

        public override void Draw(Main main)
        {
            base.Draw(main);
            main.spriteBatch.DrawCenteredString(Textures.Font, Text, Position + Size / 2f, TextColor);
        }
    }
}
