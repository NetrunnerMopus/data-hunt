namespace model.cards.types
{
    public class Identity : IType
    {
        bool IType.Playable => false;
        bool IType.Installable => false;
        bool IType.Rezzable => false;
    }
}