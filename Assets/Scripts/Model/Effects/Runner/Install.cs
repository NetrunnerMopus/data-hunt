﻿using model.cards;

namespace model.effects.runner
{
    public class Install : IEffect
    {
        private Card card;

        public Install(Card card)
        {
            this.card = card;
        }

        void IEffect.Resolve(Game game)
        {
            game.runner.zones.rig.zone.Add(card);
            game.runner.zones.grip.Remove(card);
            card.Activation.Resolve(game);
        }

        void IEffect.Observe(IImpactObserver observer, Game game)
        {
            observer.NotifyImpact(true, this);
        }
    }
}