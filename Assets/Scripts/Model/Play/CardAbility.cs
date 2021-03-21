using model.cards;

namespace model.play
{
    public class CardAbility
    {
        public Ability Ability { get; }
        public Card Card { get; }

        public CardAbility(Ability ability, Card card)
        {
            Ability = ability;
            Card = card;
        }
    }
}
