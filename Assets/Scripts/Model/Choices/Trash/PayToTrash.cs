using model.cards;
using model.effects.runner;
using model.play;

namespace model.choices.trash
{
    public class PayToTrash : ITrashOption
    {
        private ICost cost;

        public PayToTrash(ICost cost)
        {
            this.cost = cost;
        }

        void ITrashOption.Perform(Game game)
        {
            cost.Pay(game);
        }

        public Ability AsAbility(Card card)
        {
            return new Ability(cost, new TrashCorpCard(card));
        }

        public string Art => "Images/UI/trash-can";
    }
}
