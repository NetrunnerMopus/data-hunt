using model.cards;

namespace model.effects.runner
{
    public class Play : IEffect
    {
        private Card card;

        public Play(Card card)
        {
            this.card = card;
        }

        void IEffect.Resolve(Game game)
        {
            game.runner.zones.grip.Remove(card);
            card.Activation.Resolve(game);
            game.runner.zones.heap.Add(card);
        }

        void IEffect.Observe(IImpactObserver observer, Game game)
        {
            card.Activation.Observe(observer, game);
        }
    }
}