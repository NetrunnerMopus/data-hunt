using System.Collections.Generic;
using model.cards;

namespace model.zones
{
    public class Shuffling
    {
        private System.Random rng;

        public Shuffling() : this(new System.Random().Next()) { }

        public Shuffling(int seed)
        {
            rng = new System.Random(seed);
        }

        public void Shuffle(List<Card> cards)
        {
            cards.Sort((card1, card2) => rng.Next().CompareTo(rng.Next()));
        }
    }
}