using model.play;

namespace model.costs
{
    public class RunnerCreditCost : ICost
    {
        private int credits;

        public RunnerCreditCost(int credits)
        {
            this.credits = credits;
        }

        bool ICost.CanPay(Game game)
        {
            return game.runner.credits.CanPay(credits);
        }

        void ICost.Observe(IAvailabilityObserver<ICost> observer, Game game)
        {
            throw new System.NotImplementedException();
        }

        void ICost.Pay(Game game)
        {
            game.runner.credits.Pay(credits);
        }
    }
}