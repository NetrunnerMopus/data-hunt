using model.cards;
using model.cards.types;
using model.player;
using System.Linq;
using System.Threading.Tasks;

namespace model.zones.runner
{
    public class Rig : IInstallDestination
    {
        public readonly Zone zone = new Zone("Rig");

        private Game game;
        private IPilot pilot;

        public Rig(Game game, IPilot pilot)
        {
            this.game = game;
            this.pilot = pilot;
        }

        void IInstallDestination.Host(Card card)
        {
            card.MoveTo(zone);
        }

        async Task IInstallDestination.TrashAlike(Card card)
        {
            // CR: 8.2.5.d
            if (card.Type is Program && false /* disable until we get proper MU-based trashing below */)
            {
                var programs = zone.Cards.Where(it => it.Type is Program);
                var old = await pilot.ChooseACard().Declare("Which program to trash?", programs); // actually declare zero or many, unless MU is constrained then a minimum
                old.MoveTo(game.runner.zones.heap.zone); // TODO reuse an actual trash abstraction
            }
        }

        async Task IInstallDestination.PayInstallCost(Card card)
        {
            // CR: 8.2.11.a
            await card.PlayCost.Pay();
        }
    }
}
