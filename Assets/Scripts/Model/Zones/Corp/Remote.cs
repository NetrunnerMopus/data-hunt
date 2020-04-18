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
        private Game game;

        public Remote(Game game)
        {
            this.game = game;
            Ice = new IceColumn(game);
        }

        public void InstallWithin(Card card)
        {
            Zone
                .Cards
                .Select(it => new Trash(it, game.corp.zones.archives.Zone))
                .ToList()
                .ForEach(it => it.TrashIt());
            card.MoveTo(Zone);
        }

        void IInstallDestination.Host(Card card)
        {
            InstallWithin(card);
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
                var card = await pilot.ChooseACard().Declare("Which card to access now?", unaccessed, game);
                unaccessed.Remove(card);
                await new AccessCard(card, game).AwaitEnd();
            }
        }

    }
}
