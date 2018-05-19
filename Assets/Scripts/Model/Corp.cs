using model.play.corp;
using model.timing.corp;

namespace model
{
    public class Corp
    {
        public readonly Turn turn;
        public readonly ActionCard actionCard;
        public readonly ClickPool clicks;
        public readonly CreditPool credits;

        public Corp(Turn turn, ActionCard actionCard, ClickPool clicks, CreditPool credits)
        {
            this.turn = turn;
            this.actionCard = actionCard;
            this.clicks = clicks;
            this.credits = credits;
        }

        public void StartGame()
        {
            credits.Gain(5);
        }
    }
}