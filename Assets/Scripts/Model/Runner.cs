using model.cards;
using model.play.runner;
using model.player;
using model.zones.runner;

namespace model
{
    public class Runner
    {
        public readonly IPilot pilot;
        public readonly ActionCard actionCard;
        public int tags = 0;
        public readonly Zones zones;
        public readonly ClickPool clicks;
        public readonly CreditPool credits;
        public readonly Card identity;

        public Runner(
            IPilot pilot,
            ActionCard actionCard,
            int tags,
            Zones zones,
            ClickPool clicks,
            CreditPool credits,
            Card identity
        )
        {
            this.pilot = pilot;
            this.actionCard = actionCard;
            this.tags = tags;
            this.zones = zones;
            this.clicks = clicks;
            this.credits = credits;
            this.identity = identity;
        }

        public void Start(Game game)
        {
            pilot.Play(game);
            credits.Gain(5);
            zones.stack.Shuffle();
            zones.stack.Draw(5, zones.grip);
            zones.grip.ObserveAdditions(actionCard);
        }
    }
}