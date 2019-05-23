using System.Collections.Generic;
using model.cards;

namespace model.zones.corp
{
    public class ResearchAndDevelopment : IServer
    {
        public Zone Zone { get; }
        public IceColumn Ice => new IceColumn();
        private Deck deck;
        public ResearchAndDevelopment(Deck deck)
        {
            this.deck = deck;
            Zone = new Zone("R&D", deck.cards);
        }

        public void Shuffle()
        {
            deck.Shuffle();
        }

        public bool HasCards() => deck.Size() > 0;

        public void Draw(int cards, Headquarters hq)
        {
            for (int i = 0; i < cards; i++)
            {
                if (HasCards())
                {
                    hq.Zone.Add(RemoveTop());
                }
            }
        }

        private Card RemoveTop()
        {
            var top = Zone.Cards[0];
            Zone.Remove(top);
            return top;
        }
    }
}