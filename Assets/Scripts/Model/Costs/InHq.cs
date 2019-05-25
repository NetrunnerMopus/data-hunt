using model.cards;
using model.zones;
using System.Collections.Generic;

namespace model.costs
{
    public class InHq : ICost
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
            card.ObserveMoves(
                delegate (Card card, Zone source, Zone target)
                {
                    if (card == this.card)
                    {
                        NotifyInHq(target == hq);
                    }
                }
            );
        }

        private void NotifyInHq(bool inHq)
        {
            foreach (var observer in observers)
            {
                observer.NotifyPayable(inHq, this);
            }
        }

        void ICost.Pay(Game game)
        {
        }
    }
}