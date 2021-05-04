using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using model.costs;
using model.play;
using model.player;

namespace model
{
    public interface IEffect
    {
        Task Resolve();
        void Disable();
        bool Impactful { get; }
        event Action<IEffect, bool> ChangedImpact;
        IEnumerable<string> Graphics { get; }
    }

    static class IEffectExtensions
    {
        public static Ability ToMandatoryAbility(this IEffect effect, IPilot controller)
        {
            return new Ability(new Free(), effect, controller);
        }
    }
}
