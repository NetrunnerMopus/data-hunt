using model.effects.runner;
using model.play.runner;
using model.timing;
using System.Threading.Tasks;
using view;

namespace model
{
    public class Runner
    {
        private Game game;
        public readonly ActionCard actionCard;
        public int tags = 0;
        public readonly Grip grip;
        public readonly Stack stack;
        public readonly Heap heap;
        public readonly Rig rig;
        public readonly ClickPool clicks;
        public readonly CreditPool credits;

        public Runner(Game game, ActionCard actions, Grip grip, Stack stack, Heap heap, Rig rig, ClickPool clicks, CreditPool credits)
        {
            this.game = game;
            this.actionCard = actions;
            this.grip = grip;
            this.stack = stack;
            this.heap = heap;
            this.rig = rig;
            this.clicks = clicks;
            this.credits = credits;
        }

        async public Task StartGame()
        {
            credits.Gain(5);
            stack.Shuffle();
            ((IEffect)new Draw(5)).Resolve(game);
            var turn = new RunnerTurn(game);
            await turn.Start();
        }

        public bool RemoveTag()
        {
            throw new System.Exception("Not implemented yet");
        }

        public bool Run()
        {
            throw new System.Exception("Not implemented yet");
        }
    }
}