namespace model.cards.types
{
    public class Resource : IType
    {
        bool IType.Playable => false;
        bool IType.Installable => true;
        bool IType.Rezzable => false;
    }
}