using model.play;
using System.Collections.Generic;

namespace model.costs
{
    public class RunnerClickCost : ICost, IClickObserver
    {
        private int clicks;
        private HashSet<IAvailabilityObserver<ICost>> observers = new HashSet<IAvailabilityObserver<ICost>>();

        public RunnerClickCost(int clicks)
        {
            this.clicks = clicks;
        }

        bool ICost.CanPay(Game game)
        {
            return game.runner.clicks.CanSpend(clicks);
        }

        void ICost.Observe(IAvailabilityObserver<ICost> observer, Game game)
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
            foreach (var observer in observers)
            {
                observer.Notify(unspent >= clicks, this);
            }
        }
    }
}