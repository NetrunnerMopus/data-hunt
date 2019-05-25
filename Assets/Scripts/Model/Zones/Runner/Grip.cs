using model.cards;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace model.zones.runner
{
    public class Grip
    {
        public readonly Zone zone = new Zone("Grip");
        public int Count => cards.Count;
        private List<Card> cards = new List<Card>();
        private HashSet<IGripDiscardObserver> discards = new HashSet<IGripDiscardObserver>();
        private TaskCompletionSource<bool> discarded;

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
            card.MoveTo(heap.zone);
            foreach (var observer in discards)
            {
                observer.NotifyDiscarding(false);
            }
            discarded.SetResult(true);
        }

        public Card Find<T>() where T : Card => cards.OfType<T>().First();

        public void ObserveDiscarding(IGripDiscardObserver observer)
        {
            discards.Add(observer);
        }

        public void UnobserveDiscarding(IGripDiscardObserver observer)
        {
            discards.Remove(observer);
        }
    }

    public interface IGripDiscardObserver
    {
        void NotifyDiscarding(bool discarding);
    }
}
