using model.cards;
using System.Collections.Generic;

namespace model.zones.corp
{
    public class Remote : IServer, IInstallDestination
    {
        public Zone Zone { get; } = new Zone("Remote");
        public IceColumn Ice { get; } = new IceColumn();

        public void InstallWithin(Card card)
        {
            card.MoveTo(Zone);
        }

        void IInstallDestination.Host(Card card)
        {
            InstallWithin(card);
        }
    }
}
