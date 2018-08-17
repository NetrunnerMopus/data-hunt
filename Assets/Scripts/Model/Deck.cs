using System.Collections.Generic;
using model.cards;

namespace model
{
    public class Deck
    {
        private List<Card> cards;
        public readonly Card identity;
        private System.Random rng;

        public Deck(List<Card> cards, Card identity) : this(cards, identity, new System.Random().Next()) { }

        public Deck(List<Card> cards, Card identity, int seed)
        {
            this.cards = cards;
            this.identity = identity;
            rng = new System.Random(seed);
        }

        public void Shuffle()
        {
            cards.Sort((card1, card2) => rng.Next().CompareTo(rng.Next()));
        }

        public Card RemoveTop()
        {
            if (HasCards())
            {
                Card drawn = cards[0];
                cards.RemoveAt(0);
                return drawn;
            }
            else
            {
                throw new System.Exception("Trying to draw from an empty deck");
            }
        }

        public bool HasCards()
        {
            return cards.Count > 0;
        }

        public int Size()
        {
            return cards.Count;
        }
    }
}