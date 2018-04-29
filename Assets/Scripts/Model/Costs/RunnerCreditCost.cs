using model.play;
using System.Collections.Generic;

namespace model.costs
{
    public class RunnerCreditCost : ICost, IBalanceObserver
    {
        private int credits;
        private HashSet<IAvailabilityObserver<ICost>> observers = new HashSet<IAvailabilityObserver<ICost>>();

        public RunnerCreditCost(int credits)
        {
            this.credits = credits;
        }

        bool ICost.CanPay(Game game)
        {
            return game.runner.credits.CanPay(credits);
        }

        void ICost.Observe(IAvailabilityObserver<ICost> observer, Game game)
        {
            observers.Add(observer);
            game.runner.credits.Observe(this);
        }

        void ICost.Pay(Game game)
        {
            game.runner.credits.Pay(credits);
        }

        public void NotifyBalance(int balance)
        {
            var payable = balance >= credits;
            foreach (var observer in observers)
            {
                observer.Notify(payable, this);
            }
        }
    }
}