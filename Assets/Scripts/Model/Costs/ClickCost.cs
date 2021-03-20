using System;
using System.Threading.Tasks;

namespace model.costs
{
    public class ClickCost : ICost
    {
        private int clicks;
        private ClickPool pool;
        bool ICost.Payable => pool.Remaining >= clicks;
        public event Action<ICost, bool> ChangedPayability = delegate { };

        public ClickCost(ClickPool pool, int clicks)
        {
            this.pool = pool;
            this.clicks = clicks;
            pool.Changed += NotifyClicks;
        }

        private void NotifyClicks(ClickPool pool)
        {
            var payable = pool.Remaining >= clicks;
            ChangedPayability(this, payable);
        }

        async Task ICost.Pay()
        {
            pool.Spend(clicks);
            await Task.CompletedTask;
        }
    }
}
