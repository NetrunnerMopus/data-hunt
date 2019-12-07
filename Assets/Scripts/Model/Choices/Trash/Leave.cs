namespace model.choices.trash
{
    public class Leave : ITrashOption
    {
        bool ITrashOption.IsLegal(Game game) => true;

        void ITrashOption.Perform(Game game) { }

        string ITrashOption.Art => "Images/UI/thumb-up";
    }
}
