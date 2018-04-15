using model.cards;
using System.Collections.Generic;
using view;

namespace model
{
    public class Rig
    {
        private List<ICard> cards = new List<ICard>();
        private RigGrid grid;

        public Rig(RigGrid grid)
        {
            this.grid = grid;
        }

        public void Install(ICard card)
        {
            cards.Add(card);
            grid.Place(card);
        }
    }
}
