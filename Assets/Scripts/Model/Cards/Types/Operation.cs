namespace model.cards.types
{
    public class Operation : IType
    {
        bool IType.Playable { get { return true; } }

        bool IType.Installable { get { return false; } }
    }
}