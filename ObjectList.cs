using System;

namespace Slip
{
    public class ObjectList<T> where T : ListObject
    {
        private ListNode<T> first;
        private ListNode<T> last;

        public ObjectList()
        {
            first = new ListNode<T>(null, null, null);
            last = new ListNode<T>(null, first, last);
            first.next = last;
        }

        public void Add(T value)
        {
            ListNode<T> newNode = new ListNode<T>(value, last.previous, last);
            last.previous.next = newNode;
            last.previous = newNode;
        }

        public void Update(Room room, Player player)
        {
            ListNode<T> node = first.next;
            while (node != last)
            {
                if (node.value.Update(room, player))
                {
                    node.previous.next = node.next;
                    node.next.previous = node.previous;
                }
                node = node.next;
            }
        }

        public void Draw(GameScreen screen, Main main)
        {
            ListNode<T> node = first.next;
            while (node != last)
            {
                node.value.Draw(screen, main);
                node = node.next;
            }
        }

        public void Clear()
        {
            first.next = last;
            last.previous = first;
        }
    }

    internal class ListNode<T> where T : ListObject
    {
        internal T value;
        internal ListNode<T> previous;
        internal ListNode<T> next;

        internal ListNode(T value, ListNode<T> previous, ListNode<T> next)
        {
            this.value = value;
            this.previous = previous;
            this.next = next;
        }
    }

    public abstract class ListObject
    {
        /** Whether this object should be removed from the list */
        public abstract bool Update(Room room, Player player);

        public abstract void Draw(GameScreen screen, Main main);
    }
}
