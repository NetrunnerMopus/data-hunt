using System.Threading.Tasks;
using model.cards;

namespace model.effects.runner
{
    public class Install : IEffect
    {
        private Card card;

        public Install(Card card)
        {
            this.card = card;
        }

        async Task IEffect.Resolve(Game game)
        {
            card.MoveTo(game.runner.zones.rig.zone);
            await card.Activate(game);
        }

        void IEffect.Observe(IImpactObserver observer, Game game)
        {
            observer.NotifyImpact(true, this);
        }
    }
}