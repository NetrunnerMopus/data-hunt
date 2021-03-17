using System;
using System.Threading.Tasks;

namespace model.costs
{
    public class CreditCost : ICost
    {
        private CreditPool pool;
        private int credits;
        public event Action<ICost, bool> PayabilityChanged = delegate { };

        public CreditCost(CreditPool pool, int credits)
        {
            this.credits = credits;
            pool.Changed += NotifyBalance;
        }

        private void NotifyBalance(CreditPool pool)
        {
            var payable = pool.Balance >= credits;
            PayabilityChanged(this, payable);
        }

        bool ICost.Payable(Game game)
        {
            return pool.Balance >= credits;
        }

        async Task ICost.Pay(Game game)
        {
            pool.Pay(credits);
            await Task.CompletedTask;
        }
    }
}
