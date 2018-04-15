using model.cards;
using System.Collections.Generic;
using view;

namespace model
{
    public class Rig
    {
        private List<ICard> cards = new List<ICard>();
        private IRigView view;

        public Rig(IRigView view)
        {
            this.view = view;
        }

        public void Install(ICard card)
        {
            cards.Add(card);
            view.Place(card);
        }
    }
}
