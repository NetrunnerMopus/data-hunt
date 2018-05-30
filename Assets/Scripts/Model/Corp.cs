using model.play.corp;
using model.zones.corp;

namespace model
{
    public class Corp
    {
        public readonly ActionCard actionCard;
        public readonly Zones zones;
        public readonly ClickPool clicks;
        public readonly CreditPool credits;

        public Corp(ActionCard actionCard, Zones zones, ClickPool clicks, CreditPool credits)
        {
            this.actionCard = actionCard;
            this.zones = zones;
            this.clicks = clicks;
            this.credits = credits;
        }

        public void Start(Game game)
        {
            credits.Gain(5);
            zones.rd.Shuffle();
            zones.rd.Draw(5, zones.hq);
        }
    }
}