using model.cards;

namespace model.effects.runner
{
    public class Install : IEffect
    {
        private ICard card;

        public Install(ICard card)
        {
            this.card = card;
        }

        void IEffect.Resolve(Game game)
        {
            game.runner.rig.Install(card);
            game.runner.grip.Remove(card);
        }

        void IEffect.Observe(IImpactObserver observer, Game game)
        {
            observer.NotifyImpact(true, this);
        }
    }
}