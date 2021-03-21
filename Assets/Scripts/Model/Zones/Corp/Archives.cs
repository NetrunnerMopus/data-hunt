using model.cards.types;
using model.player;
using model.timing;
using System.Linq;
using System.Threading.Tasks;

namespace model.zones.corp
{
    public class Archives : IServer
    {
        public Zone Zone { get; } = new Zone("Archives", false);
        public IceColumn Ice { get; }

        public Archives(CreditPool credits)
        {
            Ice = new IceColumn(this, credits);
        }

        async Task IServer.Access(int accessCount, IPilot pilot, Game game)
        {
            var interestingCards = Zone.Cards.Where(it => it.Type is Agenda).ToList();
            while (interestingCards.Count > 0) // TODO actually access the rest of the cards too, e.g. for Obelus-HadesShard
            {
                var cardToAccess = await pilot.ChooseACard().Declare("Which card to access now?", interestingCards);
                interestingCards.Remove(cardToAccess);
                await new AccessCard(cardToAccess, game).AwaitEnd();
            }
        }
    }
}
