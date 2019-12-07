namespace model.choices.trash
{
    public interface ITrashOption
    {
        bool IsLegal(Game game);
        void Perform(Game game);
        string Art { get; }
    }
}