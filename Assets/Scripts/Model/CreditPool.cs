using System.Collections.Generic;
using view;

namespace model
{
    public class CreditPool
    {
        private int credits = 0;
        private ICreditPoolView view;
        public readonly List<IBalanceObserver> observers = new List<IBalanceObserver>();

        public CreditPool(ICreditPoolView view)
        {
            this.view = view;
        }

        public bool CanPay(int cost)
        {
            return credits >= cost;
        }

        public void Pay(int cost)
        {
            if (CanPay(cost))
            {
                credits -= cost;
                view.UpdateBalance(credits);
            }
            else
            {
                throw new System.Exception("Cannot pay " + cost + " credits while there's only " + credits + " in the pool");
            }
        }

        public void Gain(int income)
        {
            credits += income;
            view.UpdateBalance(credits);
        }

        private void UpdateBalance(int newBalance)
        {
            credits = newBalance;
            view.UpdateBalance(newBalance);
            foreach (IBalanceObserver observer in observers)
            {
                observer.Notify(newBalance);
            }
        }
    }
}