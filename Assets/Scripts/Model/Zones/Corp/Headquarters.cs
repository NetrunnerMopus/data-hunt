using model.cards;
using System.Collections.Generic;

namespace model.zones.corp
{
    public class Headquarters
    {
        private List<ICard> cards = new List<ICard>();
        private HashSet<IHqCountObserver> counts = new HashSet<IHqCountObserver>();

        public void Add(ICard card)
        {
            cards.Add(card);
            NotifyCount();
        }

        public void Remove(ICard card)
        {
            cards.Remove(card);
            NotifyCount();
        }
        
        private void NotifyCount()
        {
            foreach (var observer in counts)
            {
                observer.NotifyCardCount(cards.Count);
            }
        }

        public void ObserveCount(IHqCountObserver observer)
        {
            counts.Add(observer);
        }
    }

    public interface IHqCountObserver
    {
        void NotifyCardCount(int cards);
    }
}
