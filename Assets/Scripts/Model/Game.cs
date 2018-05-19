using model.timing;

namespace model
{
    public class Game
    {
        public Corp corp;
        public readonly Runner runner;
        public bool ended;

        public Game(Deck runnerDeck)
        {
            corp = CreateCorp();
            runner = CreateRunner(runnerDeck);
        }

        private Corp CreateCorp()
        {
            return new Corp(new play.corp.ActionCard(), new ClickPool(), new CreditPool());
        }

        private Runner CreateRunner(Deck runnerDeck)
        {
            var actionCard = new play.runner.ActionCard();
            var grip = new Grip();
            var stack = new Stack(runnerDeck);
            var heap = new Heap();
            var rig = new Rig();
            var clicks = new ClickPool();
            var credits = new CreditPool();
            return new Runner(this, actionCard, grip, stack, heap, rig, clicks, credits);
        }

        async public void Start()
        {
            corp.StartGame();
            runner.StartGame();
            await new GameFlow(this).Start();
        }
    }
}