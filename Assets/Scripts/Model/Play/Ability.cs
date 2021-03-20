using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace model.play
{
    public class Ability
    {
        public readonly ICost cost;
        public readonly IEffect effect;
        public event Action<Ability, bool> UsabilityChanged = delegate { };
        public event Action<Ability> Resolved = delegate { };
        private bool payable = false;
        private bool impactful = false;
        public bool Usable => payable && impactful;

        public Ability(ICost cost, IEffect effect)
        {
            this.cost = cost;
            this.effect = effect;
            cost.ChangedPayability += UpdateCost;
            effect.ChangedImpact += UpdateEffect;
        }

        private void UpdateCost(ICost source, bool payable)
        {
            this.payable = payable;
            UsabilityChanged(this, Usable);
        }

        private void UpdateEffect(IEffect source, bool impactful)
        {
            this.impactful = impactful;
            UsabilityChanged(this, Usable);
        }

        async public Task Trigger()
        {
            await cost.Pay();
            await effect.Resolve();
            Resolved(this);
        }

        public override bool Equals(object obj)
        {
            var ability = obj as Ability;
            return ability != null &&
                   EqualityComparer<ICost>.Default.Equals(cost, ability.cost) &&
                   EqualityComparer<IEffect>.Default.Equals(effect, ability.effect);
        }

        public override int GetHashCode()
        {
            var hashCode = 1540118860;
            hashCode = hashCode * -1521134295 + EqualityComparer<ICost>.Default.GetHashCode(cost);
            hashCode = hashCode * -1521134295 + EqualityComparer<IEffect>.Default.GetHashCode(effect);
            return hashCode;
        }

        public override string ToString() => "Ability(cost=" + cost + ", effect=" + effect + ")";
    }
}
