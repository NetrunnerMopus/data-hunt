using model.cards;
using System.Collections.Generic;

namespace model.zones.corp
{
    public class Archives
    {
        private List<Card> cards = new List<Card>();
        private HashSet<IArchivesObserver> observers = new HashSet<IArchivesObserver>();

        public void Add(Card card)
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
        void NotifyCardAdded(Card card);
    }
}
