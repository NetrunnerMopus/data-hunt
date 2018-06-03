using model.cards;

namespace model.costs
{
    public class SelfTrash : ICost
    {
        private Card card;

        public SelfTrash(Card card)
        {
            this.card = card;
        }

        void ICost.Observe(IPayabilityObserver observer, Game game)
        {
            observer.NotifyPayable(true, this);
        }

        void ICost.Pay(Game game)
        {
            game.runner.zones.rig.Uninstall(card);
            game.runner.zones.heap.Add(card);
        }
    }
}