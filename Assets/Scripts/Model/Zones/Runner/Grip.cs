using model.cards;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace model.zones.runner
{
    public class Grip
    {
        public int Count => cards.Count;
        private List<Card> cards = new List<Card>();
        private HashSet<IGripAdditionObserver> additions = new HashSet<IGripAdditionObserver>();
        private HashSet<IGripRemovalObserver> removals = new HashSet<IGripRemovalObserver>();
        private HashSet<IGripDiscardObserver> discards = new HashSet<IGripDiscardObserver>();
        private TaskCompletionSource<bool> discarded;

        public void Add(Card card)
        {
            cards.Add(card);
            foreach (var observer in additions)
            {
                observer.NotifyCardAdded(card);
            }
        }

        public void Remove(Card card)
        {
            cards.Remove(card);
            foreach (var observer in removals)
            {
                observer.NotifyCardRemoved(card);
            }
        }

        async public Task Discard()
        {
            discarded = new TaskCompletionSource<bool>();
            foreach (var observer in discards)
            {
                observer.NotifyDiscarding(true);
            }
            await discarded.Task;
        }

        public void Discard(Card card, Heap heap)
        {
            Remove(card);
            heap.Add(card);
            foreach (var observer in discards)
            {
                observer.NotifyDiscarding(false);
            }
            discarded.SetResult(true);
        }

        public Card Find<T>() where T : Card => cards.OfType<T>().First();

        public void ObserveAdditions(IGripAdditionObserver observer)
        {
            additions.Add(observer);
        }

        public void ObserveRemovals(IGripRemovalObserver observer)
        {
            removals.Add(observer);
        }

        public void ObserveDiscarding(IGripDiscardObserver observer)
        {
            discards.Add(observer);
        }

        public void UnobserveDiscarding(IGripDiscardObserver observer)
        {
            discards.Remove(observer);
        }
    }

    public interface IGripAdditionObserver
    {
        void NotifyCardAdded(Card card);
    }

    public interface IGripRemovalObserver
    {
        void NotifyCardRemoved(Card card);
    }

    public interface IGripDiscardObserver
    {
        void NotifyDiscarding(bool discarding);
    }
}
