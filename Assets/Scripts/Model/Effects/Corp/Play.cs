using model.cards;

namespace model.effects.corp
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
            card.Faceup = true;
            game.corp.zones.hq.Remove(card);
            card.Activation.Resolve(game);
            game.corp.zones.archives.Add(card);
        }

        void IEffect.Observe(IImpactObserver observer, Game game)
        {
            card.Activation.Observe(observer, game);
        }
    }
}