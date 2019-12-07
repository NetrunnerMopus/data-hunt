using model.cards;

namespace model.choices.trash
{
    public class PayToTrash : ITrashOption
    {
        private ICost cost;
        private Card card;

        public PayToTrash(ICost cost, Card card)
        {
            this.cost = cost;
            this.card = card;
        }

        bool ITrashOption.IsLegal(Game game)
        {
            return cost.Payable(game);
        }

        void ITrashOption.Perform(Game game)
        {
            cost.Pay(game);
            card.MoveTo(game.corp.zones.archives.Zone);
        }

        string ITrashOption.Art => "Images/UI/trash-can";
    }
}
