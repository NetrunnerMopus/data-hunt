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
        public bool Payable => card.Zone == zone;
        public event Action<ICost, bool> ChangedPayability = delegate { };

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
                ChangedPayability(this, target == zone);
            }
        }

        async Task ICost.Pay() => await Task.CompletedTask;
    }
}
