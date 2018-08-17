using model.player;
using model.timing;

namespace model
{
    public class Game
    {
        public readonly Corp corp;
        public readonly Runner runner;
        public readonly GameFlow flow;

        public Game(Player corpPlayer, Player runnerPlayer)
        {
            corp = CreateCorp(corpPlayer);
            runner = CreateRunner(runnerPlayer);
            var corpTurn = new timing.corp.Turn(this);
            var runnerTurn = new timing.runner.Turn(this);
            flow = new GameFlow(corpTurn, runnerTurn);
        }

        private Corp CreateCorp(Player player)
        {
            var zones = new zones.corp.Zones(
                new zones.corp.Headquarters(),
                new zones.corp.ResearchAndDevelopment(player.deck),
                new zones.corp.Archives()
            );
            var actionCard = new play.corp.ActionCard(zones);
            var clicks = new ClickPool();
            var credits = new CreditPool();
            return new Corp(player.pilot, actionCard, zones, clicks, credits, player.deck.identity);
        }

        private Runner CreateRunner(Player player)
        {
            var actionCard = new play.runner.ActionCard();
            var zones = new zones.runner.Zones(
                new zones.runner.Grip(),
                new zones.runner.Stack(player.deck),
                new zones.runner.Heap(),
                new zones.runner.Rig()
            );
            var clicks = new ClickPool();
            var credits = new CreditPool();
            return new Runner(player.pilot, actionCard, 0, zones, clicks, credits, player.deck.identity);
        }

        async public void Start()
        {
            corp.Start(this);
            runner.Start(this);
            await flow.Start();
        }
    }
}