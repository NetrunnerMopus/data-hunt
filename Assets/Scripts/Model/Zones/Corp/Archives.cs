using model.cards;
using System.Collections.Generic;

namespace model.zones.corp
{
    public class Archives
    {
        private List<ICard> cards = new List<ICard>();

        public void Add(ICard card)
        {
            cards.Add(card);
        }
    }
}
