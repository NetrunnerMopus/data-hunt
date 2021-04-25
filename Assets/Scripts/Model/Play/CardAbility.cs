using System.Threading.Tasks;
using model.cards;

namespace model.play
{
    public class CardAbility : IPlayOption
    {
        public Ability Ability { get; }
        public Card Card { get; }
        public bool Legal => Ability.Legal;

        public CardAbility(Ability ability, Card card)
        {
            Ability = ability;
            Card = card;
        }

        async public Task Resolve() => await Ability.Resolve();
    }
}
