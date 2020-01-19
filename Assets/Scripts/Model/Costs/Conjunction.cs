using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

        bool ICost.Payable(Game game) => costs.All(it => it.Payable(game));

        async Task ICost.Pay(Game game)
        {
            foreach (var cost in costs)
            {
                await cost.Pay(game);
            }
        }

        public override string ToString() => "Conjunction(costs=" + String.Join(", ", costs.ToList()) + ")";
    }
}