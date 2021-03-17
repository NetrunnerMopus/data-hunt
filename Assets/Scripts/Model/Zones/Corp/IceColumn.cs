using System.Threading.Tasks;
using model.cards;
using model.costs;

namespace model.zones.corp
{
    public class IceColumn : IInstallDestination
    {
        public int Height { get; private set; } = 0;
        private IServer server;
        private Costs costs;
        public IceColumn(IServer server, Costs costs)
        {
            this.server = server;
            this.costs = costs;
        }

        void IInstallDestination.Host(Card card)
        {
            throw new System.NotImplementedException();
        }

        Task IInstallDestination.TrashAlike(Card card)
        {
            // CR: 8.2.5.c
            throw new System.NotImplementedException();
        }

        Task IInstallDestination.PayInstallCost(Card card)
        {
            // CR: 8.2.11.a    
            costs.InstallIce(this).Pay(Height);
            return Task.CompletedTask;
        }
    }
}
