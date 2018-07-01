namespace model.cards.types
{
    public class Asset : IType
    {
        bool IType.Playable => false;
        bool IType.Installable => true;
        bool IType.Rezzable => true;
    }
}