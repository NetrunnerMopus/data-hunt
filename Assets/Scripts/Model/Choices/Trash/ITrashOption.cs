using model.cards;
using model.play;

namespace model.choices.trash
{
    public interface ITrashOption
    {
        void Perform(Game game);
        Ability AsAbility(Card card);
        string Art { get; }
    }
}