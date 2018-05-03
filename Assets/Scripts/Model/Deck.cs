using System.Collections.Generic;
using model.cards;

namespace model
{
    public class Deck
    {
        private System.Random rng;

        private List<ICard> cards;

        public Deck(List<ICard> cards) : this(cards, new System.Random().Next()) { }

        public Deck(List<ICard> cards, int seed)
        {
            this.cards = cards;
            rng = new System.Random(seed);
        }

        public void Shuffle()
        {
            cards.Sort((card1, card2) => rng.Next().CompareTo(rng.Next()));
        }

        public ICard RemoveTop()
        {
            if (HasCards())
            {
                ICard drawn = cards[0];
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