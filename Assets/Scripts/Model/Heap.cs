using model.cards;
using System.Collections.Generic;
using view;

namespace model
{
    public class Heap
    {
        private List<ICard> cards = new List<ICard>();
        private IHeapView view;

        public Heap(IHeapView view)
        {
            this.view = view;
        }

        public void Add(ICard card)
        {
            cards.Add(card);
            view.Add(card);
        }
    }
}
