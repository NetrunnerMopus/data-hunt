using model.play.runner;
using model.zones.runner;

namespace model
{
    public class Runner
    {
        public readonly ActionCard actionCard;
        public int tags = 0;
        public readonly Zones zones;
        public readonly ClickPool clicks;
        public readonly CreditPool credits;

        public Runner(ActionCard actionCard, int tags, Zones zones, ClickPool clicks, CreditPool credits)
        {
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
    }
}