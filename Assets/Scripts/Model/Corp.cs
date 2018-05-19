using model.play.corp;

namespace model
{
    public class Corp
    {
        public readonly ActionCard actionCard;
        public readonly ClickPool clicks;
        public readonly CreditPool credits;

        public Corp(ActionCard actionCard, ClickPool clicks, CreditPool credits)
        {
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