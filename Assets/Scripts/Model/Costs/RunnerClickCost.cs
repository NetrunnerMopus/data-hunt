using System.Collections.Generic;
using System.Threading.Tasks;

namespace model.costs
{
    public class RunnerClickCost : ICost, IClickObserver
    {
        private int clicks;
        private HashSet<IPayabilityObserver> observers = new HashSet<IPayabilityObserver>();

        public RunnerClickCost(int clicks)
        {
            this.clicks = clicks;
        }

        void ICost.Observe(IPayabilityObserver observer, Game game)
        {
            observers.Add(observer);
            game.runner.clicks.Observe(this);
        }

        bool ICost.Payable(Game game)
        {
            return game.runner.clicks.Remaining >= clicks;
        }

        async Task ICost.Pay(Game game)
        {
            game.runner.clicks.Spend(clicks);
            await Task.CompletedTask;
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
