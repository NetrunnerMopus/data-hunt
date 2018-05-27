using model.cards;

namespace model.costs
{
    public class SelfTrash : ICost
    {
        private ICard card;

        public SelfTrash(ICard card)
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
        }
    }
}