using model;

namespace tests.observers
{
    class LastBalanceObserver : IBalanceObserver
    {
        public int LastBalance { get; private set; }

        void IBalanceObserver.NotifyBalance(int balance)
        {
            LastBalance = balance;
        }
    }
}