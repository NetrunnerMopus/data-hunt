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
        public bool Payable => card.Zone != bin; // TODO cannot trash a card removed from the game
        public event Action<ICost, bool> ChangedPayability = delegate { };

        public Trash(Card card, Zone bin)
        {
            this.card = card;
            this.bin = bin;
            card.Moved += CheckIfTrashable;
        }

        private void CheckIfTrashable(Card card, Zone source, Zone destination)
        {
            ChangedPayability(this, Payable);
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
