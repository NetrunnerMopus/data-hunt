namespace model.costs
{
    public class RunnerCreditCost : ICost
    {
        private int credits;

        public RunnerCreditCost(int credits)
        {
            this.credits = credits;
        }

        bool ICost.Pay(Game game)
        {
            CreditPool creditPool = game.runner.credits;
            if (creditPool.CanPay(credits))
            {
                creditPool.Pay(credits);
                return true;
            }
            else
            {
                return false;
            }
        }
    }

}