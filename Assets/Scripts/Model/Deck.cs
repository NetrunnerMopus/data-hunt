using System.Collections.Generic;
using model.cards;

namespace model
{
    public class Deck
    {
        public readonly List<Card> cards;
        public readonly Card identity;

        public Deck(List<Card> cards, Card identity) { 
            this.cards = cards;
            this.identity = identity;
        }
    }
}