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

        void ICost.Pay(Game game)
        {
            game.runner.credits.Pay(credits);
        }
    }
}