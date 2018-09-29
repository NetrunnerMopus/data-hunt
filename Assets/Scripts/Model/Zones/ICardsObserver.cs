using model.cards;
using System.Collections.Generic;

namespace model.zones
{
    public interface ICardsObserver
    {
        void NotifyCards(List<Card> cards);
    }
}
