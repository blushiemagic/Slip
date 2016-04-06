using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace Slip
{
    public abstract class Screen : Component
    {
        public override void Initialize(Main main)
        {
            size = main.Size();
        }

        public override sealed void Update(Main main)
        {
            size = main.Size();
            UpdateScreen(main);
            base.Update(main);
        }

        public virtual void UpdateScreen(Main main)
        {
        }

        public override sealed void Draw(Main main)
        {
            size = main.Size();
            DrawScreen(main);
            base.Draw(main);
        }

        public virtual void DrawScreen(Main main)
        {
        }

        public virtual Color BackgroundColor()
        {
            return Color.Black;
        }

        public virtual void LoadContent(ContentManager loader)
        {
        }
    }
}
