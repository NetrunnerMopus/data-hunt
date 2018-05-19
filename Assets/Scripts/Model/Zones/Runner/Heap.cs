using model.cards;
using System.Collections.Generic;

namespace model.zones.runner
{
    public class Heap
    {
        private List<ICard> cards = new List<ICard>();
        private HashSet<IHeapObserver> observers = new HashSet<IHeapObserver>();

        public void Add(ICard card)
        {
            cards.Add(card);
            foreach (var observer in observers)
            {
                observer.NotifyCardAdded(card);
            }
        }

        public void Observe(IHeapObserver observer)
        {
            observers.Add(observer);
        }
    }

    public interface IHeapObserver
    {
        void NotifyCardAdded(ICard card);
    }
}
