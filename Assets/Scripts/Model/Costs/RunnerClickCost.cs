namespace model.costs
{
    public class RunnerClickCost : ICost
    {
        private int clicks;

        public RunnerClickCost(int clicks)
        {
            this.clicks = clicks;
        }

        bool ICost.CanPay(Game game)
        {
            return game.runner.clicks.CanSpend(clicks);
        }

        void ICost.Pay(Game game)
        {
            game.runner.clicks.Spend(clicks);
        }
    }
}