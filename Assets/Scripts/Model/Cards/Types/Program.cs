namespace model.cards.types
{
    public class Program : IType
    {
        bool IType.Playable { get { return false; } }

        bool IType.Installable { get { return true; } }
    }
}