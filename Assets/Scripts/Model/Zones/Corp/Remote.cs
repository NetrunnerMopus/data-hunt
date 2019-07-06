using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using model.cards;
using model.costs;
using model.player;
using model.timing;

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

        async Task IServer.Access(int accessCount, IPilot pilot, Game game)
        {
            var unaccessed = new List<Card>(Zone.Cards);
            for (var accessesLeft = accessCount; accessesLeft > 0; accessesLeft--)
            {
                var card = await pilot.ChooseACard().Declare(unaccessed);
                unaccessed.Remove(card);
                await new AccessCard(card, game).AwaitEnd();
            }
        }
    }
}
