using model.timing;

namespace model
{
    public class Game
    {
        public readonly Corp corp;
        public readonly Runner runner;
        public readonly GameFlow flow;
        public bool ended;

        public Game(Deck runnerDeck)
        {
            corp = CreateCorp();
            runner = CreateRunner(runnerDeck);
            flow = new GameFlow(this, corp.turn, runner.turn);
        }

        private Corp CreateCorp()
        {
            return new Corp(new timing.corp.Turn(this), new play.corp.ActionCard(), new ClickPool(), new CreditPool());
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
            return new Runner(this, turn, actionCard, 0, zones, clicks, credits);
        }

        async public void Start()
        {
            corp.StartGame();
            runner.StartGame();
            await flow.Start();
        }
    }
}