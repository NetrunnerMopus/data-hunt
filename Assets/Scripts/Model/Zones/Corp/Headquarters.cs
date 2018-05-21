using model.cards;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace model.zones.corp
{
    public class Headquarters
    {
        public int Count => cards.Count;
        private List<ICard> cards = new List<ICard>();
        private HashSet<IHqCountObserver> counts = new HashSet<IHqCountObserver>();
        private HashSet<IHqDiscardObserver> discards = new HashSet<IHqDiscardObserver>();
        private TaskCompletionSource<bool> discarded;
        private Random random;

        internal Headquarters()
        {
            random = new Random();
        }

        internal Headquarters(int seed)
        {
            random = new Random(seed);
        }

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

        async internal Task Discard()
        {
            discarded = new TaskCompletionSource<bool>();
            foreach (var observer in discards)
            {
                observer.NotifyDiscarding(true);
            }
            await discarded.Task;
        }

        internal void Discard(ICard card, Archives heap)
        {
            Remove(card);
            heap.Add(card);
            foreach (var observer in discards)
            {
                observer.NotifyDiscarding(false);
            }
            discarded.SetResult(true);
        }

        internal ICard Random() => cards[random.Next(0, Count)];

        public void ObserveCount(IHqCountObserver observer)
        {
            counts.Add(observer);
        }

        internal void ObserveDiscarding(IHqDiscardObserver observer)
        {
            discards.Add(observer);
        }
    }

    public interface IHqCountObserver
    {
        void NotifyCardCount(int cards);
    }

    internal interface IHqDiscardObserver
    {
        void NotifyDiscarding(bool discarding);
    }
}
