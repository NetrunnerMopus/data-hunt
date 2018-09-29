using model.cards;

namespace model.zones.corp
{
    public class Root : IInstallDestination
    {
        void IInstallDestination.Host(Card card)
        {
            throw new System.NotImplementedException();
        }
    }
}
