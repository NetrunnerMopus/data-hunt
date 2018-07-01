namespace model.cards.types
{
    public class Program : IType
    {
        bool IType.Playable => false;
        bool IType.Installable => true;
        bool IType.Rezzable => false;
    }
}