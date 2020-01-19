using System.Threading.Tasks;
using model.cards;
using model.zones;

namespace model.effects.runner
{
    public class Play : IEffect
    {
        private Card card;
        private Zone playZone = new Zone("Play");

        public Play(Card card)
        {
            this.card = card;
        }

        async Task IEffect.Resolve(Game game)
        {
            card.MoveTo(playZone);
            await card.Activate(game);
            card.Deactivate();
            card.MoveTo(game.runner.zones.heap.zone);
        }

        void IEffect.Observe(IImpactObserver observer, Game game)
        {
            card.Activation.Observe(observer, game);
        }
    }
}