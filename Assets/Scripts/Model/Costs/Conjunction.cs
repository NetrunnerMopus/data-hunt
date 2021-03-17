using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace model.costs
{
    /// <summary>
    /// A conjunction of <em>independent</em> costs.
    /// </summary>
    public class Conjunction : ICost
    {
        private ICost[] costs;
        private IDictionary<ICost, bool> payabilities = new Dictionary<ICost, bool>();
        public event Action<ICost, bool> PayabilityChanged;

        public Conjunction(params ICost[] costs)
        {
            this.costs = costs;
            foreach (var cost in costs)
            {
                cost.PayabilityChanged += UpdatePayability;
            }
        }

        private void UpdatePayability(ICost source, bool payable)
        {
            payabilities[source] = payable;
            var allPayable = payabilities.All(payability => payability.Value);
            PayabilityChanged(this, allPayable);
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
