using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using model.cards;

namespace model
{
    public class CreditPool
    {
        public int Balance { get; private set; } = 0;
        public event Action<CreditPool> Changed = delegate { };

        public void Pay(int cost)
        {
            if (Balance >= cost)
            {
                Balance -= cost;
                Changed(this);
            }
            else
            {
                throw new System.Exception("Cannot pay " + cost + " credits while there's only " + Balance + " in the pool");
            }
        }

        public void Gain(int income)
        {
            Balance += income;
            Changed(this);
        }

        public IEffect Gaining(int income)
        {
            return new Income(this, income);
        }

        private class Income : IEffect
        {
            public bool Impactful => true;
            public event Action<IEffect, bool> ChangedImpact;
            private CreditPool pool;
            private int credits;
            IEnumerable<string> IEffect.Graphics => new string[] { "Images/UI/credit" };

            public Income(CreditPool pool, int credits)
            {
                this.pool = pool;
                this.credits = credits;
            }

            async Task IEffect.Resolve(Game game)
            {
                pool.Gain(credits);
                await Task.CompletedTask;
            }
        }

        public ICost PayingFor(Card card, int credits)
        {
            return new Price(this, credits);
        }

        private class Price : ICost
        {
            private CreditPool pool;
            private int credits;
            public event Action<ICost, bool> PayabilityChanged = delegate { };

            public Price(CreditPool pool, int credits)
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
}
