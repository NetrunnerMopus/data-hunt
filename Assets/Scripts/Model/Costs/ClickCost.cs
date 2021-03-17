using System.Threading.Tasks;

namespace model.costs
{
    public class ClickCost : ICost
    {
        private int clicks;
        private ClickPool pool;
        public event System.Action<ICost, bool> PayabilityChanged = delegate { };

        public ClickCost(ClickPool pool, int clicks)
        {
            this.pool = pool;
            this.clicks = clicks;
            pool.Changed += NotifyClicks;
        }

        bool ICost.Payable(Game game) => pool.Remaining >= clicks;

        private void NotifyClicks(ClickPool pool)
        {
            var payable = pool.Remaining >= clicks;
            PayabilityChanged(this, payable);
        }

        async Task ICost.Pay(Game game)
        {
            pool.Spend(clicks);
            await Task.CompletedTask;
        }
    }
}
