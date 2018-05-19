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
            var grip = new Grip();
            var stack = new Stack(runnerDeck);
            var heap = new Heap();
            var rig = new Rig();
            var clicks = new ClickPool();
            var credits = new CreditPool();
            return new Runner(this, turn, actionCard, 0, grip, stack, heap, rig, clicks, credits);
        }

        async public void Start()
        {
            corp.StartGame();
            runner.StartGame();
            await flow.Start();
        }
    }
}