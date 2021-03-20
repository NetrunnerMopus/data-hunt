using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace model.effects.runner
{
    public class TagRemoval : IEffect
    {
        private int tags;
        private Runner runner;
        public bool Impactful => runner.tags > 0;
        public event Action<IEffect, bool> ChangedImpact; // TODO observe change in tag count
        IEnumerable<string> IEffect.Graphics => new string[] { };

        public TagRemoval(int tags, Runner runner)
        {
            this.tags = tags;
            this.runner = runner;
        }

        async Task IEffect.Resolve()
        {
            runner.tags -= tags;
            await Task.CompletedTask;
        }
    }
}
