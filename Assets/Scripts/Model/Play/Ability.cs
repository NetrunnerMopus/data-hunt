using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using model.cards;

namespace model.play
{
    public class Ability
    {
        public readonly ICost cost;
        public readonly IEffect effect;
        public event Action<Ability, bool> UsabilityChanged = delegate { };
        public event Action<Ability> Resolved = delegate { };
        public bool Usable => cost.Payable && effect.Impactful;

        public Ability(ICost cost, IEffect effect)
        {
            this.cost = cost;
            this.effect = effect;
            cost.ChangedPayability += UpdateCost;
            effect.ChangedImpact += UpdateEffect;
        }

        private void UpdateCost(ICost source, bool payable)
        {
            UsabilityChanged(this, Usable);
        }

        private void UpdateEffect(IEffect source, bool impactful)
        {
            UsabilityChanged(this, Usable);
        }

        async public Task Trigger()
        {
            await cost.Pay();
            await effect.Resolve();
            Resolved(this);
        }

        public CardAbility BelongingTo(Card card) => new CardAbility(this, card);

        public override string ToString() => "Ability(cost=" + cost + ", effect=" + effect + ")";
    }
}
