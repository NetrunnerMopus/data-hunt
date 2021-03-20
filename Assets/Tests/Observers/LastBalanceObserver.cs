using model;

namespace tests.observers
{
    class LastBalanceObserver
    {
        public int LastBalance { get; private set; }

        public void RememberBalance(CreditPool credits)
        {
            LastBalance = credits.Balance;
        }
    }
}
