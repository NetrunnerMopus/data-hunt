using System.Threading.Tasks;
using model.cards;

namespace model.effects.runner
{
    public class TrashCorpCard : IEffect
    {
        private Card card;

        public TrashCorpCard(Card card)
        {
            this.card = card;
        }

        async Task IEffect.Resolve(Game game)
        {
            card.MoveTo(game.corp.zones.archives.Zone);
            await Task.CompletedTask;
        }

        void IEffect.Observe(IImpactObserver observer, Game game)
        {
            observer.NotifyImpact(true, this);
        }
    }
}