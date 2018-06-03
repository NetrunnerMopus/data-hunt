using model.cards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace model.zones.corp
{
    public class Headquarters
    {
        public int Count => cards.Count;
        private List<Card> cards = new List<Card>();
        private HashSet<IHqCountObserver> counts = new HashSet<IHqCountObserver>();
        private HashSet<IHqDiscardObserver> discards = new HashSet<IHqDiscardObserver>();
        private TaskCompletionSource<bool> discarding;
        private Random random;

        public Headquarters()
        {
            random = new Random();
        }

        public Headquarters(int seed)
        {
            random = new Random(seed);
        }

        public void Add(Card card)
        {
            cards.Add(card);
            NotifyCount();
        }

        public void Remove(Card card)
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

        async public Task Discard()
        {
            discarding = new TaskCompletionSource<bool>();
            foreach (var observer in discards)
            {
                observer.NotifyDiscarding(true);
            }
            await discarding.Task;
        }

        public void Discard(Card card, Archives heap)
        {
            Remove(card);
            heap.Add(card);
            foreach (var observer in discards)
            {
                observer.NotifyDiscarding(false);
            }
            discarding.SetResult(true);
        }

        public Card Find<T>() where T : Card => cards.OfType<T>().FirstOrDefault();

        public Card Random() => cards[random.Next(0, Count)];

        public void ObserveCount(IHqCountObserver observer)
        {
            counts.Add(observer);
        }

        public void ObserveDiscarding(IHqDiscardObserver observer)
        {
            discards.Add(observer);
        }
    }

    public interface IHqCountObserver
    {
        void NotifyCardCount(int cards);
    }

    public interface IHqDiscardObserver
    {
        void NotifyDiscarding(bool discarding);
    }
}
