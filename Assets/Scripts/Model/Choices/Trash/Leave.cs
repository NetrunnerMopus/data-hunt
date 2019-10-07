using model.cards;
using model.play;

namespace model.choices.trash
{
    public class Leave : ITrashOption
    {
        public void Perform(Game game)
        {
        }

        public Ability AsAbility(Card card)
        {
            return new Ability(new model.costs.Nothing(), new model.effects.Pass());
        }

        public string Art => "Images/UI/thumb-up";
    }
}
