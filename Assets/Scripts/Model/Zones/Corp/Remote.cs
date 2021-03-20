using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using model.cards;
using model.cards.types;
using model.costs;
using model.player;
using model.timing;

namespace model.zones.corp
{
    public class Remote : IServer, IInstallDestination
    {
        public Zone Zone { get; } = new Zone("Remote");
        public IceColumn Ice { get; }
        private Archives archives;

        public Remote(Corp corp)
        {
            this.archives = corp.zones.archives;
            Ice = new IceColumn(this, corp.credits);
        }

        public bool IsEmpty() {
            return (Ice.Height == 0) && (Zone.Count == 0);
        }

        void IInstallDestination.Host(Card card)
        {
            Zone
                .Cards
                .Select(it => new Trash(it, archives.Zone))
                .ToList()
                .ForEach(it => it.TrashIt());
            card.MoveTo(Zone);
        }

        // CR: 8.2.5.a
        Task IInstallDestination.TrashAlike(Card card)
        {
            if (card.Type is Asset || card.Type is Agenda)
            {
                // TODO
            }
            return Task.CompletedTask;
        }

        Task IInstallDestination.PayInstallCost(Card card)
        {
            return Task.CompletedTask;
        }

        async Task IServer.Access(int accessCount, IPilot pilot, Game game)
        {
            var unaccessed = new List<Card>(Zone.Cards);
            for (var accessesLeft = accessCount; accessesLeft > 0; accessesLeft--)
            {
                var card = await pilot.ChooseACard().Declare("Which card to access now?", unaccessed);
                unaccessed.Remove(card);
                await new AccessCard(card, game).AwaitEnd();
            }
        }

    }
}
