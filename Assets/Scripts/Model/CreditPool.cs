using System.Collections.Generic;

namespace model
{
    public class CreditPool
    {
        public int Balance { get; private set; } = 0;
        private HashSet<IBalanceObserver> observers = new HashSet<IBalanceObserver>();

        public void Pay(int cost)
        {
            if (Balance >= cost)
            {
                Balance -= cost;
                UpdateBalance(Balance);
            }
            else
            {
                throw new System.Exception("Cannot pay " + cost + " credits while there's only " + Balance + " in the pool");
            }
        }

        public void Gain(int income)
        {
            Balance += income;
            UpdateBalance(Balance);
        }

        private void UpdateBalance(int newBalance)
        {
            Balance = newBalance;
            foreach (IBalanceObserver observer in observers)
            {
                observer.NotifyBalance(newBalance);
            }
        }

        public void Observe(IBalanceObserver observer)
        {
            observers.Add(observer);
            observer.NotifyBalance(Balance);
        }
    }

    public interface IBalanceObserver
    {
        void NotifyBalance(int balance);
    }
}