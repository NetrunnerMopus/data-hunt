using System.Threading.Tasks;
using model.cards;

namespace model.zones.corp
{
    public class Root : IInstallDestination
    {
        void IInstallDestination.Host(Card card)
        {
            throw new System.NotImplementedException();
        }

        Task IInstallDestination.PayInstallCost(Card card)
        {
            throw new System.NotImplementedException();
        }

        Task IInstallDestination.TrashAlike(Card card)
        {
            throw new System.NotImplementedException();
        }
    }
}
