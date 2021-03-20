using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace model.effects
{
    public class Nothing : IEffect
    {
        public bool Impactful => false;
        public event Action<IEffect, bool> ChangedImpact;
        async Task IEffect.Resolve() => await Task.CompletedTask;
        IEnumerable<string> IEffect.Graphics => new string[] { };
    }
}
