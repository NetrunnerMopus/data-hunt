using System.Threading.Tasks;

namespace model
{
    public interface IEffect
    {
        Task Resolve(Game game);
        void Observe(IImpactObserver observer, Game game);
    }

    public interface IImpactObserver
    {
        void NotifyImpact(bool impactful, IEffect source);
    }
}