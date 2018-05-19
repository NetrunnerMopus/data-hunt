using model.play.runner;
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
            var actionCard = new ActionCard();
            var grip = new Grip();
            var stack = new Stack(runnerDeck);
            var heap = new Heap();
            var rig = new Rig();
            var clicks = new ClickPool();
            var credits = new CreditPool();
            runner = new Runner(this, actionCard, grip, stack, heap, rig, clicks, credits);
            corp = new Corp(new CreditPool());
        }

        async public void Start()
        {
            corp.StartGame();
            runner.StartGame();
            await new GameFlow(this).Start();
        }
    }
}