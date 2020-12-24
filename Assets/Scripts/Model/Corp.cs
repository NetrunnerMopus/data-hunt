using model.cards;
using model.play.corp;
using model.player;
using model.timing;
using model.timing.corp;

namespace model
{
    public class Corp
    {
        public readonly IPilot pilot;
        public readonly CorpTurn turn;
        public readonly PaidWindow paidWindow;
        public readonly ActionCard actionCard;
        public readonly zones.corp.Zones zones;
        public readonly ClickPool clicks;
        public readonly CreditPool credits;
        public readonly Card identity;

        public Corp(
            IPilot pilot,
            CorpTurn turn,
            PaidWindow paidWindow,
            ActionCard actionCard,
            zones.corp.Zones zones,
            ClickPool clicks,
            CreditPool credits,
            Card identity
        )
        {
            this.pilot = pilot;
            this.turn = turn;
            this.paidWindow = paidWindow;
            this.actionCard = actionCard;
            this.zones = zones;
            this.clicks = clicks;
            this.credits = credits;
            this.identity = identity;
        }

        public void Start(Game game)
        {
            identity.FlipFaceUp();
            pilot.Play(game);
            credits.Gain(5);
            zones.rd.Shuffle();
            zones.rd.Draw(5, zones.hq);
        }
    }
}
