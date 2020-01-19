using System.Threading.Tasks;
using model.cards;
using model.zones;

namespace model.costs
{
    public class Trash : ICost
    {
        private readonly Card card;
        private readonly Zone bin;

        public Trash(Card card, Zone bin)
        {
            this.card = card;
            this.bin = bin;
        }

        void ICost.Observe(IPayabilityObserver observer, Game game)
        {
            observer.NotifyPayable(true, this);
        }

        bool ICost.Payable(Game game) => true;

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