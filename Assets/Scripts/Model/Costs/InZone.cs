using model.cards;
using model.zones;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace model.costs
{
    public class InZone : ICost
    {
        private Card card;
        private Zone zone;
        private HashSet<IPayabilityObserver> observers = new HashSet<IPayabilityObserver>();

        public InZone(Card card, Zone zone)
        {
            this.card = card;
            this.zone = zone;
        }

        void ICost.Observe(IPayabilityObserver observer, Game game)
        {
            observers.Add(observer);
            card.ObserveMoves(
                delegate (Card card, Zone source, Zone target)
                {
                    if (card == this.card)
                    {
                        NotifyInZone(target == zone);
                    }
                }
            );
        }

        private void NotifyInZone(bool inZone)
        {
            foreach (var observer in observers)
            {
                observer.NotifyPayable(inZone, this);
            }
        }

        bool ICost.Payable(Game game) => true;

        async Task ICost.Pay(Game game)
        {
            await Task.CompletedTask;
        }
    }
}