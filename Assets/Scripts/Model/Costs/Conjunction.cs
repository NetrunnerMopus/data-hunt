using System;
using System.Collections.Generic;
using System.Linq;

namespace model.costs
{
    /// <summary>
    /// A conjunction of <em>independent</em> costs.
    /// </summary>
    public class Conjunction : ICost, IPayabilityObserver
    {
        private ICost[] costs;
        private IDictionary<ICost, bool> payabilities = new Dictionary<ICost, bool>();
        private HashSet<IPayabilityObserver> observers = new HashSet<IPayabilityObserver>();

        public Conjunction(params ICost[] costs)
        {
            this.costs = costs;
        }

        void IPayabilityObserver.NotifyPayable(bool payable, ICost source)
        {
            payabilities[source] = payable;
            var allPayable = payabilities.All(payability => payability.Value);
            foreach (var observer in observers)
            {
                observer.NotifyPayable(allPayable, this);
            }
        }

        void ICost.Observe(IPayabilityObserver observer, Game game)
        {
            observers.Add(observer);
            Array.ForEach(costs, (cost) => cost.Observe(this, game));
        }

        void ICost.Pay(Game game)
        {
            foreach (var cost in costs)
            {
                cost.Pay(game);
            }
        }

        public override string ToString() => "Conjunction(costs=" + String.Join(", ", costs.ToList()) + ")";
    }
}