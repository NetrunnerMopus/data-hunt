using model.cards;
using System.Collections.Generic;

namespace model.zones.corp
{
    public class Archives
    {
        private List<ICard> cards = new List<ICard>();
        private HashSet<IArchivesObserver> observers = new HashSet<IArchivesObserver>();

        public void Add(ICard card)
        {
            cards.Add(card);
            foreach (var observer in observers)
            {
                observer.NotifyCardAdded(card);
            }
        }

        public void Observe(IArchivesObserver observer)
        {
            observers.Add(observer);
        }
    }

    public interface IArchivesObserver
    {
        void NotifyCardAdded(ICard card);
    }
}
