using System;
using System.Threading.Tasks;
using model.cards;
using model.zones;

namespace model.costs
{
    public class InZone : ICost
    {
        private Card card;
        private Zone zone;
        public event Action<ICost, bool> PayabilityChanged = delegate { };

        public InZone(Card card, Zone zone)
        {
            this.card = card;
            this.zone = zone;
            card.Moved += CheckIfStillInZone;
        }

        private void CheckIfStillInZone(Card card, Zone source, Zone target)
        {
            if (card == this.card)
            {
                PayabilityChanged(this, target == zone);
            }
        }

        bool ICost.Payable(Game game) => card.Zone == zone;

        async Task ICost.Pay(Game game)
        {
            await Task.CompletedTask;
        }
    }
}
