using System.Collections.Generic;

namespace model.costs
{
    public class CorpClickCost : ICost, IClickObserver
    {
        private int clicks;
        private HashSet<IPayabilityObserver> observers = new HashSet<IPayabilityObserver>();

        public CorpClickCost(int clicks)
        {
            this.clicks = clicks;
        }

        void ICost.Observe(IPayabilityObserver observer, Game game)
        {
            observers.Add(observer);
            game.corp.clicks.Observe(this);
        }

        bool ICost.Payable(Game game) => game.corp.clicks.Remaining() >= clicks;

        void ICost.Pay(Game game)
        {
            game.corp.clicks.Spend(clicks);
        }

        public void NotifyClicks(int spent, int remaining)
        {
            var payable = remaining >= clicks;
            foreach (var observer in observers)
            {
                observer.NotifyPayable(payable, this);
            }
        }
    }
}