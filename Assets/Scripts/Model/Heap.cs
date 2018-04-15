using model.cards;
using System.Collections.Generic;
using view;

namespace model
{
    public class Heap
    {
        private List<ICard> cards = new List<ICard>();
        private HeapPile pile;

        public Heap(HeapPile pile)
        {
            this.pile = pile;
        }

        public void Add(ICard card)
        {
            cards.Add(card);
            pile.Add(card);
        }
    }
}
