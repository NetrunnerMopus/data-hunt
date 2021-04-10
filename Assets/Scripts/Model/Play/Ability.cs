﻿using System;
using System.Threading.Tasks;
using model.cards;
using model.player;

namespace model.play
{
    public class Ability
    {
        public readonly ICost cost;
        public readonly IEffect effect;
        public readonly ISource source;
        public readonly IPilot controller;
        public event Action<Ability, bool> UsabilityChanged = delegate { };
        public event Action<Ability> Resolved = delegate { };
        public bool Active { get; private set; }
        public bool Usable => cost.Payable && effect.Impactful;

        public Ability(ICost cost, IEffect effect, ISource source)
        {
            this.cost = cost;
            this.effect = effect;
            this.source = source;
            this.controller = source.Controller; // CR: 1.13.5
            Active = source.Active; // CR: 9.1.8
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
            await cost.Pay(controller);
            await effect.Resolve(controller);
            Resolved(this);
        }

        public CardAbility BelongingTo(Card card) => new CardAbility(this, card);

        public override string ToString() => cost + " : " + effect;
    }
}
