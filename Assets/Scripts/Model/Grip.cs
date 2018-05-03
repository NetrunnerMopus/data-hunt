using model.cards;
using System.Collections.Generic;

namespace model
{
    public class Grip
    {
        private List<ICard> cards = new List<ICard>();
        private HashSet<IGripObserver> observers = new HashSet<IGripObserver>();

        public void Add(ICard card)
        {
            cards.Add(card);
            foreach (var observer in observers)
            {
                observer.NotifyCardAdded(card);
            }
        }

        public void Observe(IGripObserver observer)
        {
            observers.Add(observer);
        }
    }

    public interface IGripObserver
    {
        void NotifyCardAdded(ICard card);
    }
}
