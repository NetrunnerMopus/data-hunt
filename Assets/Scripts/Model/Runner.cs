using model.effects.runner;
using model.play.runner;
using model.timing.runner;

namespace model
{
    public class Runner
    {
        private Game game;
        public readonly Turn turn;
        public readonly ActionCard actionCard;
        public int tags = 0;
        public readonly Grip grip;
        public readonly Stack stack;
        public readonly Heap heap;
        public readonly Rig rig;
        public readonly ClickPool clicks;
        public readonly CreditPool credits;

        public Runner(Game game, Turn turn, ActionCard actionCard, int tags, Grip grip, Stack stack, Heap heap, Rig rig, ClickPool clicks, CreditPool credits)
        {
            this.game = game;
            this.turn = turn;
            this.actionCard = actionCard;
            this.tags = tags;
            this.grip = grip;
            this.stack = stack;
            this.heap = heap;
            this.rig = rig;
            this.clicks = clicks;
            this.credits = credits;
        }

        public void StartGame()
        {
            credits.Gain(5);
            stack.Shuffle();
            ((IEffect)new Draw(5)).Resolve(game);
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