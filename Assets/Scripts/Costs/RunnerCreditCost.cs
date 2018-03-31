using model;

namespace costs
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
            CreditPool creditPool = game.runner.creditPool;
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