using System.Collections.Generic;
using System.Threading.Tasks;

namespace model.effects
{
    public class Pass : IEffect
    {
        async Task IEffect.Resolve(Game game) => await Task.CompletedTask;
        IEnumerable<string> IEffect.Graphics => new string[] {};

        void IEffect.Observe(IImpactObserver observer, Game game)
        {
            observer.NotifyImpact(true, this);
        }
    }
}
