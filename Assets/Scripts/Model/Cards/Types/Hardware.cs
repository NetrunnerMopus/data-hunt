namespace model.cards.types
{
    public class Hardware : IType
    {
        bool IType.Playable => false;
        bool IType.Installable => true;
        bool IType.Rezzable => false;
    }
}