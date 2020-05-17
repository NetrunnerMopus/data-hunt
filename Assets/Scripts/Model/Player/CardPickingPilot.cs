using model.cards;
using model.choices;

namespace model.player
{
    public class CardPickingPilot : DelegatingPilot
    {
        private readonly IDecision<string, Card> cardPicking;

        public CardPickingPilot(IDecision<string, Card> cardPicking, IPilot basic) : base(basic)
        {
            this.cardPicking = cardPicking;
        }
        override public IDecision<string, Card> ChooseACard() => cardPicking;
    }
}