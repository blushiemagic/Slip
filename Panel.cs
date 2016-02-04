using System;
using Microsoft.Xna.Framework;

namespace Slip
{
    public class Panel : Component
    {
        private const int panelBorder = 2;

        public override void Initialize(Main main)
        {
            border = new Vector2(panelBorder, panelBorder);
        }

        public override void Draw(Main main)
        {
            Logs.ClearLog();
            Vector2 canvasSize = CanvasSize;
            Vector2 pos = Position;
            int spaceWidth = Textures.Panel.Width - 2 * panelBorder;
            int spaceHeight = Textures.Panel.Height - 2 * panelBorder;
            Rectangle dest = new Rectangle((int)pos.X, (int)pos.Y, panelBorder, panelBorder);
            Rectangle source = new Rectangle(0, 0, panelBorder, panelBorder);
            main.spriteBatch.Draw(Textures.Panel, dest, source, Color.White);
            dest.Y += panelBorder;
            dest.Height = (int)canvasSize.Y;
            source.Y += panelBorder;
            source.Height = spaceHeight;
            main.spriteBatch.Draw(Textures.Panel, dest, source, Color.White);
            dest.Y += (int)canvasSize.Y;
            dest.Height = panelBorder;
            source.Y += spaceHeight;
            source.Height = panelBorder;
            main.spriteBatch.Draw(Textures.Panel, dest, source, Color.White);
            dest.X += panelBorder;
            dest.Width = (int)canvasSize.X;
            source.X += panelBorder;
            source.Width = spaceWidth;
            main.spriteBatch.Draw(Textures.Panel, dest, source, Color.White);
            dest.Y -= (int)canvasSize.Y;
            dest.Height = (int)canvasSize.Y;
            source.Y -= spaceHeight;
            source.Height = spaceHeight;
            main.spriteBatch.Draw(Textures.Panel, dest, source, Color.White);
            dest.Y -= panelBorder;
            dest.Height = 2;
            source.Y -= panelBorder;
            source.Height = panelBorder;
            main.spriteBatch.Draw(Textures.Panel, dest, source, Color.White);
            dest.X += (int)canvasSize.X;
            dest.Width = 2;
            source.X += spaceWidth;
            source.Width = panelBorder;
            main.spriteBatch.Draw(Textures.Panel, dest, source, Color.White);
            dest.Y += panelBorder;
            dest.Height = (int)canvasSize.Y;
            source.Y += panelBorder;
            source.Height = spaceHeight;
            main.spriteBatch.Draw(Textures.Panel, dest, source, Color.White);
            dest.Y += (int)canvasSize.Y;
            dest.Height = 2;
            source.Y += spaceHeight;
            source.Height = panelBorder;
            main.spriteBatch.Draw(Textures.Panel, dest, source, Color.White);
            base.Draw(main);
        }
    }
}
