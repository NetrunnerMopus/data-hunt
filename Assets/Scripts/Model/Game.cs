using model.timing;

namespace model
{
    public class Game
    {
        public readonly Corp corp;
        public readonly Runner runner;
        public bool ended;

        public Game(Deck corpDeck, Deck runnerDeck)
        {
            corp = CreateCorp(corpDeck);
            runner = CreateRunner(runnerDeck);
        }

        private Corp CreateCorp(Deck corpDeck)
        {
            var turn = new timing.corp.Turn(this);
            var actionCard = new play.corp.ActionCard();
            var zones = new zones.corp.Zones(
                new zones.corp.Headquarters(),
                new zones.corp.ResearchAndDevelopment(corpDeck)
            );
            var clicks = new ClickPool();
            var credits = new CreditPool();
            return new Corp(turn, actionCard, zones, clicks, credits);
        }

        private Runner CreateRunner(Deck runnerDeck)
        {
            var turn = new timing.runner.Turn(this);
            var actionCard = new play.runner.ActionCard();
            var zones = new zones.runner.Zones(
                new zones.runner.Grip(),
                new zones.runner.Stack(runnerDeck),
                new zones.runner.Heap(),
                new zones.runner.Rig()
            );
            var clicks = new ClickPool();
            var credits = new CreditPool();
            return new Runner(turn, actionCard, 0, zones, clicks, credits);
        }

        async public void Start()
        {
            corp.Start(this);
            runner.Start(this);
            var flow = new GameFlow(this, corp.turn, runner.turn);
            await flow.Start();
        }
    }
}