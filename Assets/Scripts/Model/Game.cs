using model.timing;

namespace model
{
    public class Game
    {
        public readonly Corp corp;
        public readonly Runner runner;
        public readonly GameFlow flow;

        public Game(Deck corpDeck, Deck runnerDeck)
        {
            corp = CreateCorp(corpDeck);
            runner = CreateRunner(runnerDeck);
            var corpTurn = new timing.corp.Turn(this);
            var runnerTurn = new timing.runner.Turn(this);
            flow = new GameFlow(corpTurn, runnerTurn);
        }

        private Corp CreateCorp(Deck corpDeck)
        {
            var zones = new zones.corp.Zones(
                new zones.corp.Headquarters(),
                new zones.corp.ResearchAndDevelopment(corpDeck),
                new zones.corp.Archives()
            );
            var actionCard = new play.corp.ActionCard(zones);
            var clicks = new ClickPool();
            var credits = new CreditPool();
            return new Corp(actionCard, zones, clicks, credits);
        }

        private Runner CreateRunner(Deck runnerDeck)
        {

            var actionCard = new play.runner.ActionCard();
            var zones = new zones.runner.Zones(
                new zones.runner.Grip(),
                new zones.runner.Stack(runnerDeck),
                new zones.runner.Heap(),
                new zones.runner.Rig()
            );
            var clicks = new ClickPool();
            var credits = new CreditPool();
            return new Runner(actionCard, 0, zones, clicks, credits);
        }

        async public void Start()
        {
            corp.Start(this);
            runner.Start(this);
            await flow.Start();
        }
    }
}