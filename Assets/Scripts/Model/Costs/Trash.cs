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
        public event Action<ICost, bool> PayabilityChanged = delegate { };

        public Trash(Card card, Zone bin)
        {
            this.card = card;
            this.bin = bin;
        }

        bool ICost.Payable(Game game) => true; // TODO cannot trash something already in the bin (or RFG)

        async Task ICost.Pay(Game game)
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
