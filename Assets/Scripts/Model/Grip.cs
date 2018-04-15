using model.cards;
using System.Collections.Generic;
using view;

namespace model
{
    public class Grip
    {
        private List<ICard> cards = new List<ICard>();
        private IGripView view;

        public Grip(IGripView view)
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
