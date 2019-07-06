using model.cards;

namespace model.zones.corp
{
    public class IceColumn : IInstallDestination
    {
        public int Height { get; private set; } = 0;

        void IInstallDestination.Host(Card card)
        {
            throw new System.NotImplementedException();
        }
    }
}
