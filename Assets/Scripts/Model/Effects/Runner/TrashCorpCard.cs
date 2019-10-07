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

        void IEffect.Resolve(Game game)
        {
            card.MoveTo(game.corp.zones.archives.Zone);
        }

        void IEffect.Observe(IImpactObserver observer, Game game)
        {
            observer.NotifyImpact(true, this);
        }
    }
}