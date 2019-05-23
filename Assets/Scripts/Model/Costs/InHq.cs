using model.cards;
using model.zones;
using System.Collections.Generic;

namespace model.costs
{
    public class InHq : ICost, IZoneAdditionObserver, IZoneRemovalObserver
    {
        private Card card;
        private HashSet<IPayabilityObserver> observers = new HashSet<IPayabilityObserver>();

        public InHq(Card card)
        {
            this.card = card;
        }

        void ICost.Observe(IPayabilityObserver observer, Game game)
        {
            observers.Add(observer);
            var hq = game.corp.zones.hq.Zone;
            hq.ObserveAdditions(this);
            hq.ObserveRemovals(this);
        }

        void IZoneAdditionObserver.NotifyCardAdded(Card card)
        {
            if (card == this.card)
            {
                NotifyInHq(true);
            }
        }

        private void NotifyInHq(bool inHq)
        {
            foreach (var observer in observers)
            {
                observer.NotifyPayable(inHq, this);
            }
        }

        void IZoneRemovalObserver.NotifyCardRemoved(Card card)
        {
            if (card == this.card)
            {
                NotifyInHq(false);
            }
        }

        void ICost.Pay(Game game)
        {
        }
    }
}