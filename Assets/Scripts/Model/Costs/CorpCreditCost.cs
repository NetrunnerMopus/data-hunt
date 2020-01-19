using System.Collections.Generic;
using System.Threading.Tasks;

namespace model.costs
{
    public class CorpCreditCost : ICost, IBalanceObserver
    {
        private int credits;
        private HashSet<IPayabilityObserver> observers = new HashSet<IPayabilityObserver>();

        public CorpCreditCost(int credits)
        {
            this.credits = credits;
        }

        void ICost.Observe(IPayabilityObserver observer, Game game)
        {
            observers.Add(observer);
            game.corp.credits.Observe(this);
        }

        bool ICost.Payable(Game game)
        {
            return game.corp.credits.Balance >= credits;
        }

        async Task ICost.Pay(Game game)
        {
            game.corp.credits.Pay(credits);
            await Task.CompletedTask;
        }

        public void NotifyBalance(int balance)
        {
            var payable = balance >= credits;
            foreach (var observer in observers)
            {
                observer.NotifyPayable(payable, this);
            }
        }
    }
}