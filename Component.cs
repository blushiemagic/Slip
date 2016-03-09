using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Slip
{
    public class Component
    {
        public Component parent = null;
        public List<Component> children = new List<Component>();
        public Vector2 position = Vector2.Zero;
        public Vector2 alignment = Vector2.Zero;
        public Vector2 size = Vector2.Zero;
        public Vector2 border = Vector2.Zero;
        public Vector2 scaleToParent = Vector2.Zero;
        public delegate void MouseEvent(Main main);
        public event MouseEvent OnClick;

        public Vector2 Position
        {
            get
            {
                if (parent == null)
                {
                    return position;
                }
                else
                {
                    return parent.CanvasPosition + Helper.PointwiseMult(parent.CanvasSize - Size, alignment) + position;
                }
            }
        }

        public Vector2 Size
        {
            get
            {
                if (parent == null)
                {
                    return size;
                }
                else
                {
                    return Helper.PointwiseMult(parent.CanvasSize, scaleToParent) + size;
                }
            }
        }

        public Vector2 CanvasPosition
        {
            get
            {
                return Position + border;
            }
        }

        public Vector2 CanvasSize
        {
            get
            {
                return Size - 2f * border;
            }
        }

        public virtual void Initialize(Main main)
        {
        }

        public virtual void Update(Main main)
        {
            if (Main.leftMouseClick && ContainsPoint(Main.mousePos) && OnClick != null)
            {
                OnClick(main);
            }
            foreach (Component child in children)
            {
                child.Update(main);
            }
        }

        public virtual void Draw(Main main)
        {
            foreach (Component child in children)
            {
                child.Draw(main);
            }
        }

        public void AddComponent(Component child)
        {
            child.parent = this;
            children.Add(child);
            child.Initialize(Main.instance);
        }

        public bool ContainsPoint(Point point)
        {
            Vector2 pos = Position;
            Vector2 area = Size;
            return point.X >= pos.X && point.X < pos.X + area.X && point.Y >= pos.Y && point.Y < pos.Y + area.Y;
        }
    }
}
