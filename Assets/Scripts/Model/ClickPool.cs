using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace model
{
    public class ClickPool
    {
        public int NextReplenishment { get; private set; }
        private int allotted = 0;
        public int Spent { get; private set; } = 0;
        public int Remaining { get { return allotted - Spent; } }
        public event Action<ClickPool> Changed = delegate { };

        public ClickPool(int defaultReplenishment)
        {
            NextReplenishment = defaultReplenishment;
        }

        public void Spend(int clicks)
        {
            if (Remaining >= clicks)
            {
                Spent += clicks;
                Changed(this);
            }
            else
            {
                throw new Exception("Cannot spend a click, because all of them are spent");
            }
        }

        public void Replenish()
        {
            Gain(NextReplenishment);
        }

        private void Gain(int clicks)
        {
            allotted += clicks;
            Changed(this);
        }

        public void Lose(int loss)
        {
            if (allotted >= loss)
            {
                allotted -= loss;
            }
            else
            {
                allotted = 0;
            }
            Changed(this);
        }

        public void Reset()
        {
            allotted = 0;
            Spent = 0;
            Changed(this);
        }

        public IEffect Losing(int clicksToLose) => new LoseClicks(this, clicksToLose);

        private class LoseClicks : IEffect
        {
            public bool Impactful { get; private set; }
            public event System.Action<IEffect, bool> ChangedImpact = delegate { };
            private ClickPool pool;
            private int clicksToLose;
            IEnumerable<string> IEffect.Graphics => new string[] { "" };

            public LoseClicks(ClickPool pool, int clicks)
            {
                this.pool = pool;
                this.clicksToLose = clicks;
                pool.Changed += CountClicks;
            }

            private void CountClicks(ClickPool pool)
            {
                Impactful = pool.Remaining > clicksToLose;
                ChangedImpact(this, Impactful);
            }

            async Task IEffect.Resolve(Game game)
            {
                pool.Lose(clicksToLose);
                await Task.CompletedTask;
            }
        }
    }
}
