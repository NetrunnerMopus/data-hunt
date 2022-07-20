using System.Threading.Tasks;
using model.cards;

namespace model.zones.corp
{
    public class IceColumn : IInstallDestination
    {
        public int Height { get; private set; } = 0;
        private IServer server;
        private CreditPool credits;
        public IceColumn(IServer server, CreditPool credits)
        {
            this.server = server;
            this.credits = credits;
        }

        Task IInstallDestination.Host(Card card)
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
            credits.PayingForPlaying(card, Height);
            return Task.CompletedTask;
        }
    }
}
