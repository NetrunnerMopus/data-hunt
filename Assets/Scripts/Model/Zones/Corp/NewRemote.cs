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

        void IInstallDestination.Host(Card card)
        {
            var newRemote = zones.CreateRemote() as IInstallDestination;
            newRemote.Host(card);
        }
    }
}
