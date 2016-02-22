using System;

namespace Slip
{
    public class ParticleList
    {
        private ParticleListNode first;
        private ParticleListNode last;

        public ParticleList()
        {
            first = new ParticleListNode(null, null, null);
            last = new ParticleListNode(null, first, null);
            first.next = last;
        }

        public void AddParticle(Particle particle)
        {
            ParticleListNode newNode = new ParticleListNode(particle, last.previous, last);
            last.previous.next = newNode;
            last.previous = newNode;
        }

        public void UpdateParticles(Room room)
        {
            ParticleListNode node = first.next;
            while (node != last)
            {
                if (node.particle.Update(room))
                {
                    node.previous.next = node.next;
                    node.next.previous = node.previous;
                }
                node = node.next;
            }
        }

        public void DrawParticles(GameScreen screen, Main main)
        {
            ParticleListNode node = first.next;
            while (node != last)
            {
                node.particle.Draw(screen, main);
                node = node.next;
            }
        }
    }

    internal class ParticleListNode
    {
        internal Particle particle;
        internal ParticleListNode previous;
        internal ParticleListNode next;

        internal ParticleListNode(Particle particle, ParticleListNode previous, ParticleListNode next)
        {
            this.particle = particle;
            this.previous = previous;
            this.next = next;
        }
    }
}
