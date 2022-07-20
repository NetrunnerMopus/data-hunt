using System.Threading.Tasks;
using model.cards;

namespace model.zones.corp
{
    public class NewRemote : IInstallDestination
    {
        private Zones zones;

        public NewRemote(Zones zones)
        {
            this.zones = zones;
        }

        async Task IInstallDestination.Host(Card card)
        {
            var newRemote = zones.CreateRemote() as IInstallDestination;
            await newRemote.Host(card);
        }

        Task IInstallDestination.PayInstallCost(Card card)
        {
            return Task.CompletedTask;
        }

        Task IInstallDestination.TrashAlike(Card card)
        {
            return Task.CompletedTask;
        }
    }
}
