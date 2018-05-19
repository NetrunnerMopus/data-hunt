using model.cards;

namespace model.effects.runner
{
    public class Play : IEffect
    {
        private ICard card;

        public Play(ICard card)
        {
            this.card = card;
        }

        void IEffect.Resolve(Game game)
        {
            card.PlayEffect.Resolve(game);
            game.runner.zones.grip.Remove(card);
        }

        void IEffect.Observe(IImpactObserver observer, Game game)
        {
            card.PlayEffect.Observe(observer, game);
        }
    }
}