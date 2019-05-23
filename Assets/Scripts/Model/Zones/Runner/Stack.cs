using model.cards;
using model.zones.corp;
using System.Collections.Generic;

namespace model.zones.runner
{
    public class Stack
    {
        public readonly Zone Zone;
        private Deck deck;
        public Stack(Deck deck)
        {
            this.deck = deck;
            Zone = new Zone("Stack", deck.cards);
        }

        public void Shuffle()
        {
            deck.Shuffle();
        }

        public bool HasCards() => deck.Size() > 0;

        public void Draw(int cards, Grip grip)
        {
            for (int i = 0; i < cards; i++)
            {
                if (HasCards())
                {
                    grip.Add(RemoveTop());
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