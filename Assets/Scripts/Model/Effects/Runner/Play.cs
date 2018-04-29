using model.cards;

namespace model.effects.runner
{
    public class PlayEventFromGrip : IEffect
    {
        private ICard card;

        public PlayEventFromGrip(ICard card)
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