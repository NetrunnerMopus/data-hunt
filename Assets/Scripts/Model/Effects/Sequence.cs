using System;
using System.Collections.Generic;
using System.Linq;

namespace model.effects
{
    public class Sequence : IEffect, IImpactObserver
    {
        private IEffect[] effects;
        private IDictionary<IEffect, bool> impacts = new Dictionary<IEffect, bool>();
        private HashSet<IImpactObserver> observers = new HashSet<IImpactObserver>();

        public Sequence(params IEffect[] effects)
        {
            this.effects = effects;
        }

        void IEffect.Resolve(Game game)
        {
            foreach (var effect in effects)
            {
                effect.Resolve(game);
            }
        }

        void IEffect.Observe(IImpactObserver observer, Game game)
        {
            observers.Add(observer);
            Array.ForEach(effects, (effects) => effects.Observe(this, game));
        }

        void IImpactObserver.NotifyImpact(bool impactful, IEffect source)
        {
            impacts[source] = impactful;
            var anyImpact = impacts.Any(impact => impact.Value);
            foreach (var observer in observers)
            {
                observer.NotifyImpact(anyImpact, this);
            }
        }
    }
}