using System;
using System.Threading.Tasks;
using model.cards;
using model.zones;

namespace model.costs
{
    public class Trash : ICost
    {
        private readonly Card card;
        private readonly Zone bin;
        bool ICost.Payable => true; // TODO cannot trash something already in the bin (or RFG)
        public event Action<ICost, bool> ChangedPayability = delegate { };

        public Trash(Card card, Zone bin)
        {
            this.card = card;
            this.bin = bin;
        }

        async Task ICost.Pay()
        {
            TrashIt();
            await Task.CompletedTask;
        }

        public void TrashIt()
        {
            card.Deactivate();
            card.MoveTo(bin);
        }
    }
}
