using System.Linq;
using model.cards;
using model.costs;

namespace model.zones.corp
{
    public class Remote : IServer, IInstallDestination
    {
        public Zone Zone { get; } = new Zone("Remote");
        public IceColumn Ice { get; } = new IceColumn();
        private Zone archives;

        public Remote(Zone archives)
        {
            this.archives = archives;
        }

        public void InstallWithin(Card card)
        {
            Zone
                .Cards
                .Select(it => new Trash(it, archives))
                .ToList()
                .ForEach(it => it.TrashIt());
            card.MoveTo(Zone);
        }

        void IInstallDestination.Host(Card card)
        {
            InstallWithin(card);
        }
    }
}
