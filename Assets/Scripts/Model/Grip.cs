using model.cards;
using System.Collections.Generic;

namespace model
{
    public class Grip
    {
        private List<ICard> cards = new List<ICard>();
        private HashSet<IGripAdditionObserver> additions = new HashSet<IGripAdditionObserver>();
        private HashSet<IGripRemovalObserver> removals = new HashSet<IGripRemovalObserver>();

        public void Add(ICard card)
        {
            cards.Add(card);
            foreach (var observer in additions)
            {
                observer.NotifyCardAdded(card);
            }
        }

        public void Remove(ICard card)
        {
            cards.Remove(card);
            foreach (var observer in removals)
            {
                observer.NotifyCardRemoved(card);
            }
        }

        public void ObserveAdditions(IGripAdditionObserver observer)
        {
            additions.Add(observer);
        }

        public void ObserveRemovals(IGripRemovalObserver observer)
        {
            removals.Add(observer);
        }
    }

    public interface IGripAdditionObserver
    {
        void NotifyCardAdded(ICard card);
    }

    public interface IGripRemovalObserver
    {
        void NotifyCardRemoved(ICard card);
    }
}
