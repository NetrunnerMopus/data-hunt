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
            card.PlayCost.Pay(game);
            card.PlayEffect.Resolve(game);
        }
    }
}