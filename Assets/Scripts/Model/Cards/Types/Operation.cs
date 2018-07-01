namespace model.cards.types
{
    public class Operation : IType
    {
        bool IType.Playable => true;
        bool IType.Installable => false;
        bool IType.Rezzable => false;
    }
}