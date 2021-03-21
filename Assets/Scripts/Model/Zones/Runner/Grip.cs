using model.cards;
using model.player;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace model.zones.runner
{
    public class Grip
    {
        public readonly Zone zone = new Zone("Grip");
        private IList<IGripDiscardObserver> discards = new List<IGripDiscardObserver>();
        private TaskCompletionSource<bool> discarded;

        public Grip()
        {
            zone.Added += (zone, card) => card.UpdateInfo(Information.HIDDEN_FROM_CORP); 
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
            card.MoveTo(heap.zone);
            foreach (var observer in discards)
            {
                observer.NotifyDiscarding(false);
            }
            discarded.SetResult(true);
        }

        public Card Find<T>() where T : Card => zone.Cards.OfType<T>().First();

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
