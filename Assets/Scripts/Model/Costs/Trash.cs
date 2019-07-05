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

        void ICost.Pay(Game game)
        {
            TrashIt();
        }

        public void TrashIt()
        {
            card.Deactivate();
            card.MoveTo(bin);
        }
    }
}