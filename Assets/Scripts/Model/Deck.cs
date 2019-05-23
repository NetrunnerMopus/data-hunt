using System.Collections.Generic;
using model.cards;

namespace model
{
    public class Deck
    {
        public readonly List<Card> cards;
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

        public int Size()
        {
            return cards.Count;
        }
    }
}