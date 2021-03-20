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

        public IEffect Gaining(int income) => new Income(this, income);

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

            async Task IEffect.Resolve()
            {
                pool.Gain(credits);
                await Task.CompletedTask;
            }
        }
        public ICost Paying(int credits) => new Price(this, credits);
        public ICost PayingForPlaying(Card card, int credits) => new Price(this, credits);
        public ICost PayingForTrashing(Card card, int trashCost) => new Price(this, trashCost);

        private class Price : ICost
        {
            private CreditPool pool;
            private int credits;
            public bool Payable => pool.Balance >= credits;
            public event Action<ICost, bool> ChangedPayability = delegate { };

            public Price(CreditPool pool, int credits)
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
}
