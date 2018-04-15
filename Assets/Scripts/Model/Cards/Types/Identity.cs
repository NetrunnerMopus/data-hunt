namespace model.cards.types
{
    public class Identity : IType
    {
        bool IType.Playable { get { return false; } }

        bool IType.Installable { get { return false; } }
    }
}