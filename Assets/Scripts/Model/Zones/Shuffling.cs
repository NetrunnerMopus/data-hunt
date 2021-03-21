using System;
using System.Collections.Generic;
using model.cards;

namespace model.zones
{
    public class Shuffling
    {
        public Random Random { get; }

        public Shuffling() : this(new Random().Next()) { }

        public Shuffling(int seed)
        {
            Random = new Random(seed);
        }

        public void Shuffle(List<Card> cards)
        {
            cards.Sort((card1, card2) => Random.Next().CompareTo(Random.Next()));
        }
    }
}
