using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace model.effects
{
    public class Sequence : IEffect
    {
        private IEffect[] effects;
        private IDictionary<IEffect, bool> impacts = new Dictionary<IEffect, bool>();
        public bool Impactful => impacts.Any(impact => impact.Value);
        public event Action<IEffect, bool> ChangedImpact;
        IEnumerable<string> IEffect.Graphics => effects.SelectMany(it => it.Graphics).ToList();

        public Sequence(params IEffect[] effects)
        {
            this.effects = effects;
            foreach (var effect in effects)
            {
                effect.ChangedImpact += UpdateImpact;
            }
        }

        private void UpdateImpact(IEffect effect, bool impactful)
        {
            impacts[effect] = impactful;
            ChangedImpact(this, Impactful);
        }

        async Task IEffect.Resolve()
        {
            foreach (var effect in effects)
            {
                await effect.Resolve();
            }
        }
    }
}
