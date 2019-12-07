using model.cards;
using model.zones;
using System.Collections.Generic;

namespace model.costs
{
    public class Active : ICost
    {
        private Card card;
        private HashSet<IPayabilityObserver> observers = new HashSet<IPayabilityObserver>();

        public Active(Card card)
        {
            this.card = card;
            card.ObserveActivity(
                delegate ()
                {
                    foreach (var observer in observers)
                    {
                        observer.NotifyPayable(card.Active, this);
                    }
                }
            );
        }

        void ICost.Observe(IPayabilityObserver observer, Game game)
        {
            observers.Add(observer);
            observer.NotifyPayable(card.Active, this);
        }

        bool ICost.Payable(Game game) => card.Active;

        void ICost.Pay(Game game)
        {
        }
    }
}