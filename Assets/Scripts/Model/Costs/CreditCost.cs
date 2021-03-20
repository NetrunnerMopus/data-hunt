using System;
using System.Threading.Tasks;

namespace model.costs
{
    public class CreditCost : ICost
    {
        private CreditPool pool;
        private int credits;
        public bool Payable => pool.Balance >= credits;
        public event Action<ICost, bool> ChangedPayability = delegate { };

        public CreditCost(CreditPool pool, int credits)
        {
            this.credits = credits;
            pool.Changed += NotifyBalance;
        }

        private void NotifyBalance(CreditPool pool)
        {
            var payable = pool.Balance >= credits;
            ChangedPayability(this, payable);
        }

        async Task ICost.Pay()
        {
            pool.Pay(credits);
            await Task.CompletedTask;
        }
    }
}
