using System.Threading.Tasks;

namespace model.effects
{
    public class Nothing : IEffect
    {
        async Task IEffect.Resolve(Game game) => await Task.CompletedTask;

        void IEffect.Observe(IImpactObserver observer, Game game)
        {
            observer.NotifyImpact(false, this);
        }
    }
}