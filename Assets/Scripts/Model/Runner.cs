using model.effects.runner;
using model.play.runner;
using model.timing.runner;
using model.zones.runner;

namespace model
{
    public class Runner
    {
        public readonly Turn turn;
        public readonly ActionCard actionCard;
        public int tags = 0;
        public readonly Zones zones;
        public readonly ClickPool clicks;
        public readonly CreditPool credits;

        public Runner(Turn turn, ActionCard actionCard, int tags, Zones zones, ClickPool clicks, CreditPool credits)
        {
            this.turn = turn;
            this.actionCard = actionCard;
            this.tags = tags;
            this.zones = zones;
            this.clicks = clicks;
            this.credits = credits;
        }

        public void Start(Game game)
        {
            credits.Gain(5);
            zones.stack.Shuffle();
            zones.stack.Draw(5, zones.grip);
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