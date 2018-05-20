using model.effects.corp;
using model.play.corp;
using model.timing.corp;
using model.zones.corp;

namespace model
{
    public class Corp
    {
        public readonly Turn turn;
        public readonly ActionCard actionCard;
        public readonly Zones zones;
        public readonly ClickPool clicks;
        public readonly CreditPool credits;

        public Corp(Turn turn, ActionCard actionCard, Zones zones, ClickPool clicks, CreditPool credits)
        {
            this.turn = turn;
            this.actionCard = actionCard;
            this.zones = zones;
            this.clicks = clicks;
            this.credits = credits;
        }

        public void Start(Game game)
        {
            credits.Gain(5);
            zones.rd.Shuffle();
            ((IEffect)new Draw(5)).Resolve(game);
        }
    }
}