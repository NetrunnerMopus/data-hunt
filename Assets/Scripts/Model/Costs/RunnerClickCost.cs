﻿using System.Collections.Generic;

namespace model.costs
{
    public class RunnerClickCost : ICost, IClickObserver
    {
        private int clicks;
        private HashSet<IPayabilityObserver> observers = new HashSet<IPayabilityObserver> ();

        public RunnerClickCost(int clicks)
        {
            this.clicks = clicks;
        }

        bool ICost.CanPay(Game game)
        {
            return game.runner.clicks.CanSpend(clicks);
        }

        void ICost.Observe(IPayabilityObserver observer, Game game)
        {
            observers.Add(observer);
            game.runner.clicks.Observe(this);
        }

        void ICost.Pay(Game game)
        {
            game.runner.clicks.Spend(clicks);
        }

        public void NotifyClicks(int spent, int unspent)
        {
            var payable = unspent >= clicks;
            foreach (var observer in observers)
            {
                observer.NotifyPayable(payable, this);
            }
        }
    }
}