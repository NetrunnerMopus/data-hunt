using view;

namespace model
{
    public class CreditPool
    {
        private int credits = 0;
        private ICreditPoolView view;

        public CreditPool(ICreditPoolView view)
        {
            this.view = view;
        }

        public int MaxPayout()
        {
            return credits;
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
    }
}