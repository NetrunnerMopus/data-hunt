using model.cards;
using model.zones.corp;
using System.Threading.Tasks;

namespace model.timing
{
    public class AccessCard
    {
        private readonly Card card;
        private readonly Game game;

        public AccessCard(Card card, Game game)
        {
            this.card = card;
            this.game = game;
        }

        async public Task AwaitEnd()
        {
            await TriggerAccessingCard(); // 7.8.1
            await game.Checkpoint(); // 7.8.2
            await Trash(); // 7.8.3
            await Steal(); // 7.8.4
            await TriggerAfterAccessingCard(); // 7.8.5
            await game.Checkpoint(); // 7.8.6
        }

        async private Task TriggerAccessingCard()
        {
            await Task.CompletedTask; // TODO
        }

        async private Task Trash()
        {
            await Task.CompletedTask; // TODO
        }

        async private Task Steal()
        {
            if (card is cards.corp.CorporateSalesTeam)
            {
                card.MoveTo(game.runner.zones.score);
                UnityEngine.Debug.Log("Stole CST!");

                /**
                    Maybe each card can respond to being stolen:
                    - you must steal me (Project Atlas)
                    - you can steal me if you pay me (Obokata Protocol)
                    - you cannot steal me (asset, operation, Lakshmi Smartfabrics, Old Hollywood Grid, Haarpsichord, Trebuchet etc.)
                 */
            }
            await Task.CompletedTask; // TODO
        }

        async private Task TriggerAfterAccessingCard()
        {
            await Task.CompletedTask; // TODO
        }

        public override string ToString()
        {
            return "AccessCard(card=" + card + ")";
        }
    }
}