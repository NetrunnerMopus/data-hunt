using System.Collections.Generic;
using System.Threading.Tasks;

namespace model
{
    public interface IEffect
    {
        Task Resolve(Game game);
        void Observe(IImpactObserver observer, Game game);
        IEnumerable<string> Graphics { get; }
    }

    public interface IImpactObserver
    {
        void NotifyImpact(bool impactful, IEffect source);
    }
}
