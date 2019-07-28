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

         public string Art => "Images/UI/trash-can";
    }
}
