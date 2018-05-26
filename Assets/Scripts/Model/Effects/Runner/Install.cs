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
            game.runner.zones.rig.Install(card);
            game.runner.zones.grip.Remove(card);
            card.PlayEffect.Resolve(game);
        }

        void IEffect.Observe(IImpactObserver observer, Game game)
        {
            observer.NotifyImpact(true, this);
        }
    }
}