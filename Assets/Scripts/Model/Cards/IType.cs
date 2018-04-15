namespace model.cards
{
    public interface IType
    {
        bool Playable { get; }
        bool Installable { get; }
    }
}