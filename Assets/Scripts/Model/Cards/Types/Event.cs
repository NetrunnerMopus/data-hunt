namespace model.cards.types
{
    public class Event : IType
    {
        bool IType.Playable { get { return true; } }

        bool IType.Installable { get { return false; } }
    }
}