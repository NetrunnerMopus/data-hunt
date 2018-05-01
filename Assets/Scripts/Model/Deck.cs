using System.Collections.Generic;
using model.cards;

namespace model
{
    public class Deck
    {
        private System.Random rng = new System.Random(10006);

        private List<ICard> cards;

        public Deck(List<ICard> cards)
        {
            this.cards = cards;
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