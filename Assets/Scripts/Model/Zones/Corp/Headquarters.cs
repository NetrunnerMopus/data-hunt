using model.cards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace model.zones.corp
{
    public class Headquarters : IServer
    {
        string IServer.Name => "HQ";
        public int Count => cards.Count;
        public List<Card> cards = new List<Card>();
        private HashSet<IHqAdditionObserver> additions = new HashSet<IHqAdditionObserver>();
        private HashSet<IHqRemovalObserver> removals = new HashSet<IHqRemovalObserver>();
        private HashSet<IZoneCountObserver> counts = new HashSet<IZoneCountObserver>();
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

            foreach (var observer in (new HashSet<IHqAdditionObserver>(additions)))
            {
                observer.NotifyCardAdded(card);
            }
        }

        public void Remove(Card card)
        {
            cards.Remove(card);
            NotifyCount();
            foreach (var observer in removals)
            {
                observer.NotifyCardRemoved(card);
            }
        }

        private void NotifyCount()
        {
            foreach (var observer in counts)
            {
                observer.NotifyCount(cards.Count);
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

        public void ObserveAdditions(IHqAdditionObserver observer)
        {
            additions.Add(observer);
        }

        public void ObserveRemovals(IHqRemovalObserver observer)
        {
            removals.Add(observer);
        }

        public void ObserveCount(IZoneCountObserver observer)
        {
            counts.Add(observer);
        }

        public void ObserveDiscarding(IHqDiscardObserver observer)
        {
            discards.Add(observer);
        }
    }

    public interface IHqAdditionObserver
    {
        void NotifyCardAdded(Card card);
    }

    public interface IHqRemovalObserver
    {
        void NotifyCardRemoved(Card card);
    }

    public interface IHqDiscardObserver
    {
        void NotifyDiscarding(bool discarding);
    }
}
