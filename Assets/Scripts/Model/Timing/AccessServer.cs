using model.cards;
using model.zones.corp;
using System.Threading.Tasks;

namespace model.timing
{
    public class AccessStructure
    {
        private IServer server;
        private readonly Game game;

        public AccessStructure(IServer server, Game game)
        {
            this.server = server;
            this.game = game;
        }

        async public Task AwaitEnd()
        {
            await TriggerAccessing(); // 7.6.1
            await game.Checkpoint(); // 7.6.2
            var accessCount = await DetermineNumberOfCardsToBeAccessed(); // 7.6.3
            await TurnArchivesFaceUp(); // 7.6.4
            await AccessCardSet(accessCount);  // 7.6.5, 7.6.6
            await game.Checkpoint(); // 7.6.7
        }

        async private Task TriggerAccessing()
        {
            await Task.CompletedTask; // TODO
        }

        async private Task<int> DetermineNumberOfCardsToBeAccessed()
        {
            return await Task.FromResult(1); // TODO
        }

        async private Task TurnArchivesFaceUp()
        {
            if (server == game.corp.zones.archives)
            {
                await Task.CompletedTask; // TODO
            }
        }


        async private Task AccessCardSet(int accessCount)
        {
            var accessCardSet = server.Access(accessCount, game.runner.pilot);
            foreach (var cardAccess in accessCardSet)
            {
                await AccessCard(cardAccess);
            }
        }

        async private Task AccessCard(Card card)
        {
            UnityEngine.Debug.Log("Accessing " + card);
            await Task.CompletedTask; // TODO
        }

        public override string ToString()
        {
            return "Access(server=" + server + ")";
        }
    }
}