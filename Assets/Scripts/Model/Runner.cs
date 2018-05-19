using model.effects.runner;
using model.play.runner;
using model.timing.runner;
using model.zones.runner;

namespace model
{
    public class Runner
    {
        private Game game;
        public readonly Turn turn;
        public readonly ActionCard actionCard;
        public int tags = 0;
        public readonly Zones zones;
        public readonly ClickPool clicks;
        public readonly CreditPool credits;

        public Runner(Game game, Turn turn, ActionCard actionCard, int tags, Zones zones, ClickPool clicks, CreditPool credits)
        {
            this.game = game;
            this.turn = turn;
            this.actionCard = actionCard;
            this.tags = tags;
            this.zones = zones;
            this.clicks = clicks;
            this.credits = credits;
        }

        public void StartGame()
        {
            credits.Gain(5);
            zones.stack.Shuffle();
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